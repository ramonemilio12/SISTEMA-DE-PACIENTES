using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;
using SistemaPacientes.Infrastructure.Context;

namespace SistemaPacientes.Infrastructure.Repositories;

public sealed class EspecialidadRepository : Repository<Especialidad>, IEspecialidadRepository
{
    public EspecialidadRepository(SistemaPacientesContext context) : base(context) { }

    public Task<bool> ExisteCodigoAsync(string codigo, Guid? excludeId = null, CancellationToken cancellationToken = default)
        => Set.AnyAsync(e => e.Codigo == codigo && (excludeId == null || e.Id != excludeId), cancellationToken);
}
