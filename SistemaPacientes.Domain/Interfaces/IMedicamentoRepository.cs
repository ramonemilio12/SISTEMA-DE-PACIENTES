using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Domain.Interfaces;

public interface IMedicamentoRepository : IRepository<Medicamento>
{
    Task<bool> ExisteCodigoAsync(string codigo, Guid? excludeId = null, CancellationToken cancellationToken = default);
}
