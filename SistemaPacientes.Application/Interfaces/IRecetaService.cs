using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Interfaces;

public interface IRecetaService
{
    Task<IReadOnlyList<RecetaDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RecetaDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<RecetaDto> CreateAsync(RecetaWriteDto dto, CancellationToken cancellationToken = default);
    Task<RecetaDto?> UpdateAsync(Guid id, RecetaWriteDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
