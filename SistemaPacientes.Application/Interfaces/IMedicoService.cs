using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Interfaces;

public interface IMedicoService
{
    Task<IReadOnlyList<MedicoDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MedicoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<MedicoDto> CreateAsync(MedicoWriteDto dto, CancellationToken cancellationToken = default);
    Task<MedicoDto?> UpdateAsync(Guid id, MedicoWriteDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
