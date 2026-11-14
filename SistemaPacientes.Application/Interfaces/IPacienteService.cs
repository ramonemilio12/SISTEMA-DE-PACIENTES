using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Interfaces;

public interface IPacienteService
{
    Task<IReadOnlyList<PacienteDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PacienteDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PacienteDto> CreateAsync(PacienteWriteDto dto, CancellationToken cancellationToken = default);
    Task<PacienteDto?> UpdateAsync(Guid id, PacienteWriteDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
