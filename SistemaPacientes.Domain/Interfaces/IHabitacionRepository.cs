using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Domain.Interfaces;

public interface IHabitacionRepository : IRepository<Habitacion>
{
    Task<IReadOnlyList<Habitacion>> GetDisponiblesAsync(CancellationToken cancellationToken = default);
    Task<bool> ExisteNumeroAsync(string numero, Guid? excludeId = null, CancellationToken cancellationToken = default);
}
