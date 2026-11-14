using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Interfaces;

public interface ICitaService
{
    Task<IReadOnlyList<CitaDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CitaDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CitaDto> CreateAsync(CitaWriteDto dto, CancellationToken cancellationToken = default);
    Task<CitaDto?> UpdateAsync(Guid id, CitaWriteDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
