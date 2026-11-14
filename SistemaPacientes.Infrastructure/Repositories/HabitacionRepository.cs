using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;
using SistemaPacientes.Infrastructure.Context;

namespace SistemaPacientes.Infrastructure.Repositories;

public sealed class HabitacionRepository : Repository<Habitacion>, IHabitacionRepository
{
    public HabitacionRepository(SistemaPacientesContext context) : base(context) { }

    public async Task<IReadOnlyList<Habitacion>> GetDisponiblesAsync(CancellationToken cancellationToken = default)
        => await Set.AsNoTracking().Where(h => h.Disponible).ToListAsync(cancellationToken);

    public Task<bool> ExisteNumeroAsync(string numero, Guid? excludeId = null, CancellationToken cancellationToken = default)
        => Set.AnyAsync(h => h.Numero == numero && (excludeId == null || h.Id != excludeId), cancellationToken);
}
