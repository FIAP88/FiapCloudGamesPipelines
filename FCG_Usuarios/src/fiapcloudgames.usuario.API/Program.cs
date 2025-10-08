using fiapcloudgames.usuario.Ioc;
using fiapcloudgames.usuario.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configurar Swagger com suporte a JWT
builder.Services.AddSwaggerWithJwtAuth();

// Configurar autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };

        // Configurar eventos para respostas JSON estruturadas
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";

                string detail;
                var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                
                if (string.IsNullOrEmpty(authHeader))
                {
                    detail = "Token de autenticação não fornecido. Inclua o header 'Authorization: Bearer {token}' na requisição.";
                }
                else if (!authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    detail = "Formato do token inválido. Use o formato 'Bearer {token}' no header Authorization.";
                }
                else
                {
                    detail = "Token JWT inválido ou expirado. Faça login novamente para obter um novo token.";
                }

                var response = new
                {
                    type = "https://tools.ietf.org/html/rfc7235#section-3.1",
                    title = "Não autorizado",
                    status = 401,
                    detail = detail,
                    instance = context.Request.Path.Value
                };

                var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                return context.Response.WriteAsync(jsonResponse);
            },
            OnForbidden = context =>
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                    title = "Acesso negado",
                    status = 403,
                    detail = "Você não tem permissão para acessar este recurso.",
                    instance = context.Request.Path.Value
                };

                var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                return context.Response.WriteAsync(jsonResponse);
            }
        };
    });

builder.Services.AddAuthorization();

// Adicionar CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("FiapCloudGamesPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:4200", "https://fiapcloudgames.com")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Adicionar Health Checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "FIAP Cloud Games - Usuários API v1");
        options.RoutePrefix = "swagger";
        options.DocumentTitle = "FIAP Cloud Games - API de Usuários";
        options.DefaultModelsExpandDepth(-1); // Não expandir modelos por padrão
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None); // Não expandir operações por padrão
        options.EnableDeepLinking();
        options.EnableFilter();
        options.EnableValidator();
        
        // Configurar autenticação persistente
        options.ConfigObject.AdditionalItems.Add("persistAuthorization", true);
    });
}

// Adicionar middlewares customizados na ordem correta
app.UseCustomMiddlewares();

app.UseHttpsRedirection();

// CORS deve vir antes da autenticação
app.UseCors("FiapCloudGamesPolicy");

app.UseAuthentication();
app.UseAuthorization();

// Health check endpoint
app.MapHealthChecks("/health");

// Servir arquivo CSS customizado para Swagger
app.UseStaticFiles();

app.MapControllers();
app.Run();