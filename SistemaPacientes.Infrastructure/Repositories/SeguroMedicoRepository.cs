using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;
using SistemaPacientes.Infrastructure.Context;

namespace SistemaPacientes.Infrastructure.Repositories;

public sealed class SeguroMedicoRepository : Repository<SeguroMedico>, ISeguroMedicoRepository
{
    public SeguroMedicoRepository(SistemaPacientesContext context) : base(context) { }

    public Task<bool> ExisteCodigoAsync(string codigo, Guid? excludeId = null, CancellationToken cancellationToken = default)
        => Set.AnyAsync(s => s.Codigo == codigo && (excludeId == null || s.Id != excludeId), cancellationToken);
}
