using Microsoft.OpenApi.Models;
using SistemaPacientes.Api.Middleware;
using SistemaPacientes.Application;
using SistemaPacientes.Infrastructure;
using SistemaPacientes.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Sistema de Pacientes",
        Version = "v1",
        Description = "API REST con arquitectura N-Capas (Domain, Infrastructure, Application y Api) "
                    + "que expone el CRUD del Sistema de Pacientes."
    });
});

var connectionString = builder.Configuration.GetConnectionString("SistemaPacientes")
    ?? throw new InvalidOperationException("No se configuró la cadena de conexión 'SistemaPacientes'.");

// N-Capas: se registran las capas de Infraestructura y Aplicación.
builder.Services.AddInfrastructure(connectionString);
builder.Services.AddApplication();

const string BlazorPolicy = "BlazorClient";
builder.Services.AddCors(options =>
{
    options.AddPolicy(BlazorPolicy, policy => policy
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

var app = builder.Build();

// Crea la base de datos (SQL Server LocalDB) y siembra los datos iniciales.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SistemaPacientesContext>();
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors(BlazorPolicy);
app.MapControllers();

// Endpoint de salud para comprobar que el API está en ejecución.
app.MapHealthChecks("/health");

app.Run();
