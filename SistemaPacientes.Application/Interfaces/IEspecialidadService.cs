using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Interfaces;

public interface IEspecialidadService
{
    Task<IReadOnlyList<EspecialidadDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<EspecialidadDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<EspecialidadDto> CreateAsync(EspecialidadWriteDto dto, CancellationToken cancellationToken = default);
    Task<EspecialidadDto?> UpdateAsync(Guid id, EspecialidadWriteDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
