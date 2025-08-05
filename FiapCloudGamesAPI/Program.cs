using AutenticacaoEAutorizacaoCorreto.Services;
using AutenticacaoEAutorizacaoCorreto.Services.IService;
using FiapCloudGamesAPI.Context;
using FiapCloudGamesAPI.Infra;
using FiapCloudGamesAPI.Infra.Middleware;
using FiapCloudGamesAPI.Models.Configuration;
using FiapCloudGamesAPI.Services.IService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Prometheus;

#region Configuration
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options => options.ListenAnyIP(80));
#endregion

#region Authentication & Authorization
builder.Services.AddCors();
builder.Services.AddControllers();

var key = Encoding.ASCII.GetBytes(builder.Configuration["ConfigSecret:Secret"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.PoliticasCustomizadas();
});
#endregion

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, insira 'Bearer' [espaço] e o token JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new String[]{}
        }
    });

    c.EnableAnnotations();
});
#endregion

#region Services & Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions => sqlOptions.EnableRetryOnFailure()));

builder.Services.Configure<ConfigSecret>(builder.Configuration.GetSection("ConfigSecret"));

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICacheService, MemCacheService>();
builder.Services.AddCorrelationIdGenerator();
builder.Services.AddTransient(typeof(BaseLogger<>));
builder.Services.AddHttpContextAccessor();
//builder.Services.AddScoped(IUsuarioService, UsuarioService);

builder.Services.AddMemoryCache();
#endregion

#region Application Pipeline
var app = builder.Build();
app.MapGet("/", () => Results.Text("Bem-vindo a FiapCloudGames!", "text/plain"));
#endregion

#region Middleware
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FiapCloudGamesAPI v1");
    });
}

app.UseCorrelationMiddleware();
app.UseInfoUsuarioMiddleware();
app.UseTratamentoDeErrosMiddleware();
#endregion

#region Request Pipeline
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
#endregion

#region Endpoint Mapping & Execution
app.UseHttpMetrics();
app.MapMetrics();
app.MapControllers();
app.Run();
#endregion