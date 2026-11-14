using Microsoft.Extensions.DependencyInjection;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Application.Services;

namespace SistemaPacientes.Application;

/// <summary>
/// Extensión de DI que registra los servicios de la capa de aplicación.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IEspecialidadService, EspecialidadService>();
        services.AddScoped<ISeguroMedicoService, SeguroMedicoService>();
        services.AddScoped<IHabitacionService, HabitacionService>();
        services.AddScoped<IMedicamentoService, MedicamentoService>();
        services.AddScoped<IPacienteService, PacienteService>();
        services.AddScoped<IMedicoService, MedicoService>();
        services.AddScoped<ICitaService, CitaService>();
        services.AddScoped<IConsultaService, ConsultaService>();
        services.AddScoped<IDiagnosticoService, DiagnosticoService>();
        services.AddScoped<ITratamientoService, TratamientoService>();
        services.AddScoped<IRecetaService, RecetaService>();
        services.AddScoped<IRecetaDetalleService, RecetaDetalleService>();
        services.AddScoped<IHospitalizacionService, HospitalizacionService>();

        return services;
    }
}
