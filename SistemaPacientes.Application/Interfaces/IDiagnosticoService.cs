using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Interfaces;

public interface IDiagnosticoService
{
    Task<IReadOnlyList<DiagnosticoDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DiagnosticoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<DiagnosticoDto> CreateAsync(DiagnosticoWriteDto dto, CancellationToken cancellationToken = default);
    Task<DiagnosticoDto?> UpdateAsync(Guid id, DiagnosticoWriteDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
