using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace MinhaPrimeiraAPI.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();

                //options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                //    Name = "Authorization",
                //    Scheme = "Bearer",
                //    BearerFormat = "JWT",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey
                //});

                //options.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer"
                //            }
                //        },
                //        Array.Empty<string>()
                //    }
                //});

                //string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);

                //xmlPath = Path.Combine(AppContext.BaseDirectory, "MinhaPrimeiraAPI.xml");
                //options.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        // Outra abstração
        public static WebApplication UseSwaggerConfiguration(this WebApplication app, IWebHostEnvironment environment, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // No momento que gerar o swagger, obter as versões e gera um endpoint para cada versão do grupo.
                foreach (string description in provider.ApiVersionDescriptions.Select(x => x.GroupName))
                {
                    if (environment.IsDevelopment())
                    {
                        options.SwaggerEndpoint($"/swagger/{description}/swagger.json", description.ToUpperInvariant());
                    }
                    else
                    {
                        options.SwaggerEndpoint($"/MinhaPrimeiraAPI/API/swagger/{description}/swagger.json", description.ToUpperInvariant());
                    }
                }
            });

            return app;
        }
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        // Pega todas as vesões da minha API e adicionar uma documentação para cada uma delas.
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        // Criando uma documentação minima da minha API
        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            // Info = Declaro uma instancia da classe info que é do Swagger
            var info = new OpenApiInfo()
            {
                Title = "Minha primeira API",
                Version = description.ApiVersion.ToString(),
                Description = "Esta API faz parte do projeto Minha primeira API",
                Contact = new OpenApiContact
                {
                    Name = "Seu nome",
                    Email = "e-mail",
                },
            };

            if (description.IsDeprecated)
            {
                info.Description += " Esta versão está obsoleta!";
            }

            return info;
        }
    }

    // Classe do proprio swagger
    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters is null)
                return;

            // Pegar a descrição, se está obsoleta entre outros.
            foreach (var parameter in operation.Parameters)
            {
                var description = context.ApiDescription
                    .ParameterDescriptions
                    .First(p => p.Name == parameter.Name);

                var routeInfo = description.RouteInfo;

                operation.Deprecated = OpenApiOperation.DeprecatedDefault;

                parameter.Description ??= description.ModelMetadata?.Description;

                if (routeInfo is null)
                    continue;

                if (parameter.In != ParameterLocation.Path && parameter.Schema.Default is null)
                    parameter.Schema.Default = new OpenApiString(routeInfo.DefaultValue?.ToString());

                parameter.Required |= !routeInfo.IsOptional;
            }
        }
    }
}