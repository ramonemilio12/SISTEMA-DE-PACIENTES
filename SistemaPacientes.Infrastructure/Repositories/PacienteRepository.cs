using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;
using SistemaPacientes.Infrastructure.Context;

namespace SistemaPacientes.Infrastructure.Repositories;

public sealed class PacienteRepository : Repository<Paciente>, IPacienteRepository
{
    public PacienteRepository(SistemaPacientesContext context) : base(context) { }

    public Task<Paciente?> GetByExpedienteAsync(string numeroExpediente, CancellationToken cancellationToken = default)
        => Set.AsNoTracking().FirstOrDefaultAsync(p => p.NumeroExpediente == numeroExpediente, cancellationToken);

    public async Task<IReadOnlyList<Paciente>> GetWithSeguroAsync(CancellationToken cancellationToken = default)
        => await Set.AsNoTracking().Include(p => p.SeguroMedico).ToListAsync(cancellationToken);

    public Task<Paciente?> GetDetalleAsync(Guid id, CancellationToken cancellationToken = default)
        => Set.AsNoTracking().Include(p => p.SeguroMedico).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public Task<bool> ExisteExpedienteAsync(string numeroExpediente, Guid? excludeId = null, CancellationToken cancellationToken = default)
        => Set.AnyAsync(p => p.NumeroExpediente == numeroExpediente && (excludeId == null || p.Id != excludeId), cancellationToken);

    public Task<bool> ExisteDocumentoAsync(string documento, Guid? excludeId = null, CancellationToken cancellationToken = default)
        => Set.AnyAsync(p => p.Documento == documento && (excludeId == null || p.Id != excludeId), cancellationToken);
}
