using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Interfaces;

public interface IMedicamentoService
{
    Task<IReadOnlyList<MedicamentoDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<MedicamentoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<MedicamentoDto> CreateAsync(MedicamentoWriteDto dto, CancellationToken cancellationToken = default);
    Task<MedicamentoDto?> UpdateAsync(Guid id, MedicamentoWriteDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
