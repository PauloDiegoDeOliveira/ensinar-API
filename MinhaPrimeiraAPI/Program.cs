// Services

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MinhaPrimeiraAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configurationManager = builder.Configuration;

IWebHostEnvironment environment = builder.Environment;

builder.Services.AddControllers();

builder.Services.AddDatabaseConfiguration(configurationManager);

builder.Services.AddDependencyInjectionConfiguration();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddCorsConfiguration();

builder.Services.AddVersionConfiguration();

// Configure services

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCors("Development");
}
else if (app.Environment.IsStaging())
{
    app.UseCors("Homologation");
}
else if (app.Environment.IsProduction())
{
    app.UseCors("Production");
    app.UseHsts();
}

app.UseDatabaseConfiguration();

app.UseSwaggerConfiguration(environment, provider);

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();