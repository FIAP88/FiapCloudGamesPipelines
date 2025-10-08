using Microsoft.OpenApi.Models;
using System.Reflection;

namespace fiapcloudgames.usuario.API.Extensions
{
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Configura o Swagger com suporte a autenticação JWT
        /// </summary>
        public static IServiceCollection AddSwaggerWithJwtAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // Informações básicas da API
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FIAP Cloud Games - API de Usuários",
                    Version = "v1",
                    Description = "API REST para gerenciamento de usuários da plataforma FIAP Cloud Games",
                    License = new OpenApiLicense
                    {
                        Name = "FIAP - Uso Educacional",
                        Url = new Uri("https://fiap.com.br")
                    }
                });

                // Definir o esquema de segurança JWT
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Insira o token JWT no formato: Bearer {seu-token}"
                });

                // Aplicar o requisito de segurança JWT globalmente
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });

                // Incluir comentários XML se existirem
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }

                // Configurações adicionais
                options.DescribeAllParametersInCamelCase();

                // Configurar tags
                options.TagActionsBy(api => new[] { api.GroupName ?? api.ActionDescriptor.RouteValues["controller"] });
                options.DocInclusionPredicate((name, api) => true);
            });

            return services;
        }
    }
}