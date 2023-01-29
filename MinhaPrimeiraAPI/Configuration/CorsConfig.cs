namespace MinhaPrimeiraAPI.Configuration
{
    // O CORS (Cross-origin Resource Sharing) é um mecanismo utilizado pelos navegadores para compartilhar recursos entre diferentes origens.
    // O CORS é uma especificação do W3C e faz uso de headers do HTTP para informar aos navegadores se determinado recurso pode ser ou não acessado.

    // STATIC - Na verdade um Membro Estático funciona de forma semelhante a um método "comum".
    // A maior vantagem de utiliza-lo é que não precisamos instanciar a classe para poder utiliza-lo.
    public static class CorsConfig
    {
        public static void AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                // Politica de desenvolvimento permitindo tudo.
                // CORS aplciado pelo browser
                // Já vem no asp net configurado para negar qualquer requisição de outra origem, então quando você adiciona o cors é para facilitar este acesso de fora.
                options.AddPolicy("Development", builder =>
                {
                    builder.AllowAnyMethod() // Permitir qualquer metodo.
                           .AllowAnyHeader() // Permitir qualquer tipo de header
                           .SetIsOriginAllowed(origin => true) // Permitindo qualquer origem, dominios diferentes.
                           .AllowCredentials(); // Permitir qualquer tipo de credencias.
                });

                options.AddPolicy("Production", builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .SetIsOriginAllowed(origin => true)
                           .AllowCredentials();
                });

                options.AddPolicy("Staging", builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .SetIsOriginAllowed(origin => true)
                           .AllowCredentials();
                });
            });
        }
    }
}