using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Domain.Interfaces;

public interface IEspecialidadRepository : IRepository<Especialidad>
{
    Task<bool> ExisteCodigoAsync(string codigo, Guid? excludeId = null, CancellationToken cancellationToken = default);
}
