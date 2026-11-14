using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SistemaPacientes.Domain.Interfaces;
using SistemaPacientes.Infrastructure.Context;
using SistemaPacientes.Infrastructure.Repositories;

namespace SistemaPacientes.Infrastructure;

/// <summary>
/// Extensión de DI que registra la infraestructura de datos (EF Core + repositorios).
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<SistemaPacientesContext>(options =>
            options.UseSqlServer(connectionString));

        // Repositorio genérico reutilizable por todas las entidades.
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Repositorios específicos con consultas propias.
        services.AddScoped<IEspecialidadRepository, EspecialidadRepository>();
        services.AddScoped<IPacienteRepository, PacienteRepository>();
        services.AddScoped<IMedicoRepository, MedicoRepository>();
        services.AddScoped<ICitaRepository, CitaRepository>();
        services.AddScoped<IHabitacionRepository, HabitacionRepository>();
        services.AddScoped<ISeguroMedicoRepository, SeguroMedicoRepository>();
        services.AddScoped<IMedicamentoRepository, MedicamentoRepository>();

        return services;
    }
}
