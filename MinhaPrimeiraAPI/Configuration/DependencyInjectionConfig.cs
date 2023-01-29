using Microsoft.Extensions.Options;
using MinhaPrimeiraAPI.Repositories;
using MinhaPrimeiraAPI.Repositories.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MinhaPrimeiraAPI.Configuration
{
    // Injeção de dependência: é um código que possui uma alta dependência de um outro código, onde o relacionamento entre os dois é muito forte!
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IParticipanteRepository, ParticipanteRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }
    }
}

// *** Singleton ***: é criada uma única instância para todas requisições.
// Em outras palavras, é criada uma instância a primeira vez que é solicitada e todas as vezes seguintes a mesma instância é usada (design patter singleton);

// *** Scoped: *** é criada uma única instância por requição.

// Ou seja, usando o exemplo de uma aplicação Web, quando recebe uma nova requisição, por exemplo, um click num botão do outro lado do navegador,
// é criada uma instância, onde o escopo é essa requisição. Então se for necessária a dependência multiplas vezes na mesma requisição a mesma instância será usada.
// Seria como um "Singleton para uma requisição";

// *** Transient: *** sempre é criada uma nova instância cada vez que for necessário, independentede da requisição, basicamente new XXX cada vez que for necessário.

// Um pequeno quadro para ilustrar:

// Tipo          Mesma requisição	Requisições diferentes
// Singleton	 Mesma instância	Mesma instância
// Scoped	     Mesma instância	Nova instância
// Transient	 Nova instância	    Nova instância