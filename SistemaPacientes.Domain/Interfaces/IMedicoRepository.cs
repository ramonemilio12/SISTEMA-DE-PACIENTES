using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Domain.Interfaces;

public interface IMedicoRepository : IRepository<Medico>
{
    Task<IReadOnlyList<Medico>> GetWithEspecialidadAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Medico>> GetByEspecialidadAsync(Guid especialidadId, CancellationToken cancellationToken = default);
    Task<bool> ExisteColegiadoAsync(string numeroColegiado, Guid? excludeId = null, CancellationToken cancellationToken = default);
}
