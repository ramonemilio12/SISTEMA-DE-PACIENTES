using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Interfaces;

public interface IConsultaService
{
    Task<IReadOnlyList<ConsultaDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ConsultaDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ConsultaDto> CreateAsync(ConsultaWriteDto dto, CancellationToken cancellationToken = default);
    Task<ConsultaDto?> UpdateAsync(Guid id, ConsultaWriteDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
