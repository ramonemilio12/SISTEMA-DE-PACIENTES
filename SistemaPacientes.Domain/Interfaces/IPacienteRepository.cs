using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Domain.Interfaces;

public interface IPacienteRepository : IRepository<Paciente>
{
    Task<Paciente?> GetByExpedienteAsync(string numeroExpediente, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Paciente>> GetWithSeguroAsync(CancellationToken cancellationToken = default);
    Task<Paciente?> GetDetalleAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExisteExpedienteAsync(string numeroExpediente, Guid? excludeId = null, CancellationToken cancellationToken = default);
    Task<bool> ExisteDocumentoAsync(string documento, Guid? excludeId = null, CancellationToken cancellationToken = default);
}
