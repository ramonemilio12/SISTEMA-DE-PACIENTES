using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;
using SistemaPacientes.Infrastructure.Context;

namespace SistemaPacientes.Infrastructure.Repositories;

public sealed class MedicamentoRepository : Repository<Medicamento>, IMedicamentoRepository
{
    public MedicamentoRepository(SistemaPacientesContext context) : base(context) { }

    public Task<bool> ExisteCodigoAsync(string codigo, Guid? excludeId = null, CancellationToken cancellationToken = default)
        => Set.AnyAsync(m => m.Codigo == codigo && (excludeId == null || m.Id != excludeId), cancellationToken);
}
