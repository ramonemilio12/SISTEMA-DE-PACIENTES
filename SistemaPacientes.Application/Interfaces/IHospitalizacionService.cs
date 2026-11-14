using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.Application.Interfaces;

public interface IHospitalizacionService
{
    Task<IReadOnlyList<HospitalizacionDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<HospitalizacionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<HospitalizacionDto> CreateAsync(HospitalizacionWriteDto dto, CancellationToken cancellationToken = default);
    Task<HospitalizacionDto?> UpdateAsync(Guid id, HospitalizacionWriteDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
