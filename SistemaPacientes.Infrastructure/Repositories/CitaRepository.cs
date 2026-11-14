using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;
using SistemaPacientes.Infrastructure.Context;

namespace SistemaPacientes.Infrastructure.Repositories;

public sealed class CitaRepository : Repository<Cita>, ICitaRepository
{
    public CitaRepository(SistemaPacientesContext context) : base(context) { }

    public async Task<IReadOnlyList<Cita>> GetWithDetailsAsync(CancellationToken cancellationToken = default)
        => await Set.AsNoTracking().Include(c => c.Paciente).Include(c => c.Medico).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Cita>> GetByPacienteAsync(Guid pacienteId, CancellationToken cancellationToken = default)
        => await Set.AsNoTracking().Where(c => c.PacienteId == pacienteId).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Cita>> GetByMedicoAsync(Guid medicoId, CancellationToken cancellationToken = default)
        => await Set.AsNoTracking().Where(c => c.MedicoId == medicoId).ToListAsync(cancellationToken);
}
