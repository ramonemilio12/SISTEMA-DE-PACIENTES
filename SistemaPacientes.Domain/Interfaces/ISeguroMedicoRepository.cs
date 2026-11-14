using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Domain.Interfaces;

public interface ISeguroMedicoRepository : IRepository<SeguroMedico>
{
    Task<bool> ExisteCodigoAsync(string codigo, Guid? excludeId = null, CancellationToken cancellationToken = default);
}
