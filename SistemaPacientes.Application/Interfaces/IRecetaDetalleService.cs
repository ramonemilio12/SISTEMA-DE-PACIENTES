using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Interfaces;

public interface IRecetaDetalleService
{
    Task<IReadOnlyList<RecetaDetalleDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RecetaDetalleDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<RecetaDetalleDto> CreateAsync(RecetaDetalleWriteDto dto, CancellationToken cancellationToken = default);
    Task<RecetaDetalleDto?> UpdateAsync(Guid id, RecetaDetalleWriteDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
