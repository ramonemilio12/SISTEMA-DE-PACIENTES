using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Domain.Interfaces;

public interface ICitaRepository : IRepository<Cita>
{
    Task<IReadOnlyList<Cita>> GetWithDetailsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Cita>> GetByPacienteAsync(Guid pacienteId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Cita>> GetByMedicoAsync(Guid medicoId, CancellationToken cancellationToken = default);
}
