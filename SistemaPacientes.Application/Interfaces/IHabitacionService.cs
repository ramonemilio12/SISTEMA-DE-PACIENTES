using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Interfaces;

public interface IHabitacionService
{
    Task<IReadOnlyList<HabitacionDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<HabitacionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<HabitacionDto> CreateAsync(HabitacionWriteDto dto, CancellationToken cancellationToken = default);
    Task<HabitacionDto?> UpdateAsync(Guid id, HabitacionWriteDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
