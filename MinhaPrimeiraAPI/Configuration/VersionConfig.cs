using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace MinhaPrimeiraAPI.Configuration
{
    public static class VersionConfig
    {
        public static IServiceCollection AddVersionConfiguration(this IServiceCollection services)
        {
            services.AddApiVersioning(o =>
            {
                o.UseApiBehavior = false;
                o.ReportApiVersions = true; // Quando você for consumir essa API sera informado o header do response, essa api está ok ou obsoleta.
                o.DefaultApiVersion = new ApiVersion(1, 0); // Versão default 1
                o.AssumeDefaultVersionWhenUnspecified = true; // Assuma a versão default quando não for especificado, exemplo: v1 vai assumir a default.
                o.ApiVersionReader = ApiVersionReader.Combine(
                    //new HeaderApiVersionReader("x-api-version"),
                    //new QueryStringApiVersionReader(),
                    new UrlSegmentApiVersionReader());
            });

            services.AddVersionedApiExplorer(options =>
            {
                // Versionamento Semântico

                //versão Maior(MAJOR): quando fizer mudanças incompatíveis na API,
                //versão Menor(MINOR): quando adicionar funcionalidades mantendo compatibilidade, e
                //versão de Correção(PATCH): quando corrigir falhas mantendo compatibilidade.

                options.GroupNameFormat = "'V'VVV"; // Agrupar a versão das API - V = versão, V = MAJOR, V = MINOR, V = PATCH
                options.SubstituteApiVersionInUrl = true; // Substitui as rotas "URL" pelo numero da versão padrão se não especificado
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}