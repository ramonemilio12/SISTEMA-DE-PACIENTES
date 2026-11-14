using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Interfaces;

public interface ISeguroMedicoService
{
    Task<IReadOnlyList<SeguroMedicoDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SeguroMedicoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<SeguroMedicoDto> CreateAsync(SeguroMedicoWriteDto dto, CancellationToken cancellationToken = default);
    Task<SeguroMedicoDto?> UpdateAsync(Guid id, SeguroMedicoWriteDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
