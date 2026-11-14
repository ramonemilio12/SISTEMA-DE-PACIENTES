using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Interfaces;

public interface ITratamientoService
{
    Task<IReadOnlyList<TratamientoDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TratamientoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TratamientoDto> CreateAsync(TratamientoWriteDto dto, CancellationToken cancellationToken = default);
    Task<TratamientoDto?> UpdateAsync(Guid id, TratamientoWriteDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
