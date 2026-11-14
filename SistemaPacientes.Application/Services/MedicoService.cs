using SistemaPacientes.Application.Common;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public sealed class MedicoService : IMedicoService
{
    private readonly IMedicoRepository _repository;
    private readonly IRepository<Especialidad> _especialidadRepository;

    public MedicoService(IMedicoRepository repository, IRepository<Especialidad> especialidadRepository)
    {
        _repository = repository;
        _especialidadRepository = especialidadRepository;
    }

    public async Task<IReadOnlyList<MedicoDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(Map).ToList();
    }

    public async Task<MedicoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : Map(entity);
    }

    public async Task<MedicoDto> CreateAsync(MedicoWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (await _repository.ExisteColegiadoAsync(dto.NumeroColegiado, null, cancellationToken))
        {
            throw new BusinessException($"Ya existe un médico con el número de colegiado '{dto.NumeroColegiado}'.");
        }

        if (!await _especialidadRepository.ExistsAsync(dto.EspecialidadId, cancellationToken))
        {
            throw new BusinessException("La especialidad indicada no existe.");
        }

        var entity = new Medico
        {
            NumeroColegiado = dto.NumeroColegiado.Trim(),
            Nombre = dto.Nombre.Trim(),
            Apellido = dto.Apellido.Trim(),
            Correo = dto.Correo?.Trim() ?? string.Empty,
            Telefono = dto.Telefono?.Trim() ?? string.Empty,
            EspecialidadId = dto.EspecialidadId
        };

        await _repository.AddAsync(entity, cancellationToken);
        return Map(entity);
    }

    public async Task<MedicoDto?> UpdateAsync(Guid id, MedicoWriteDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        if (await _repository.ExisteColegiadoAsync(dto.NumeroColegiado, id, cancellationToken))
        {
            throw new BusinessException($"Ya existe un médico con el número de colegiado '{dto.NumeroColegiado}'.");
        }

        if (!await _especialidadRepository.ExistsAsync(dto.EspecialidadId, cancellationToken))
        {
            throw new BusinessException("La especialidad indicada no existe.");
        }

        entity.NumeroColegiado = dto.NumeroColegiado.Trim();
        entity.Nombre = dto.Nombre.Trim();
        entity.Apellido = dto.Apellido.Trim();
        entity.Correo = dto.Correo?.Trim() ?? string.Empty;
        entity.Telefono = dto.Telefono?.Trim() ?? string.Empty;
        entity.EspecialidadId = dto.EspecialidadId;

        await _repository.UpdateAsync(entity, cancellationToken);
        return Map(entity);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);

    private static MedicoDto Map(Medico e) => new(e.Id, e.NumeroColegiado, e.Nombre, e.Apellido, e.Correo, e.Telefono, e.EspecialidadId);
}
