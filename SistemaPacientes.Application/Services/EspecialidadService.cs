using SistemaPacientes.Application.Common;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public sealed class EspecialidadService : IEspecialidadService
{
    private readonly IEspecialidadRepository _repository;

    public EspecialidadService(IEspecialidadRepository repository) => _repository = repository;

    public async Task<IReadOnlyList<EspecialidadDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(Map).ToList();
    }

    public async Task<EspecialidadDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : Map(entity);
    }

    public async Task<EspecialidadDto> CreateAsync(EspecialidadWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (await _repository.ExisteCodigoAsync(dto.Codigo, null, cancellationToken))
        {
            throw new BusinessException($"Ya existe una especialidad con el código '{dto.Codigo}'.");
        }

        var entity = new Especialidad
        {
            Codigo = dto.Codigo.Trim(),
            Nombre = dto.Nombre.Trim(),
            Descripcion = dto.Descripcion?.Trim() ?? string.Empty
        };

        await _repository.AddAsync(entity, cancellationToken);
        return Map(entity);
    }

    public async Task<EspecialidadDto?> UpdateAsync(Guid id, EspecialidadWriteDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        if (await _repository.ExisteCodigoAsync(dto.Codigo, id, cancellationToken))
        {
            throw new BusinessException($"Ya existe otra especialidad con el código '{dto.Codigo}'.");
        }

        entity.Codigo = dto.Codigo.Trim();
        entity.Nombre = dto.Nombre.Trim();
        entity.Descripcion = dto.Descripcion?.Trim() ?? string.Empty;

        await _repository.UpdateAsync(entity, cancellationToken);
        return Map(entity);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);

    private static EspecialidadDto Map(Especialidad e) => new(e.Id, e.Codigo, e.Nombre, e.Descripcion);
}
