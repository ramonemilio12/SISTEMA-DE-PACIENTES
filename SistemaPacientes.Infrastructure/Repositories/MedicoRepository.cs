using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;
using SistemaPacientes.Infrastructure.Context;

namespace SistemaPacientes.Infrastructure.Repositories;

public sealed class MedicoRepository : Repository<Medico>, IMedicoRepository
{
    public MedicoRepository(SistemaPacientesContext context) : base(context) { }

    public async Task<IReadOnlyList<Medico>> GetWithEspecialidadAsync(CancellationToken cancellationToken = default)
        => await Set.AsNoTracking().Include(m => m.Especialidad).ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Medico>> GetByEspecialidadAsync(Guid especialidadId, CancellationToken cancellationToken = default)
        => await Set.AsNoTracking().Where(m => m.EspecialidadId == especialidadId).ToListAsync(cancellationToken);

    public Task<bool> ExisteColegiadoAsync(string numeroColegiado, Guid? excludeId = null, CancellationToken cancellationToken = default)
        => Set.AnyAsync(m => m.NumeroColegiado == numeroColegiado && (excludeId == null || m.Id != excludeId), cancellationToken);
}
