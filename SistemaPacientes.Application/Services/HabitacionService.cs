using SistemaPacientes.Application.Common;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public sealed class HabitacionService : IHabitacionService
{
    private readonly IHabitacionRepository _repository;

    public HabitacionService(IHabitacionRepository repository) => _repository = repository;

    public async Task<IReadOnlyList<HabitacionDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(Map).ToList();
    }

    public async Task<HabitacionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : Map(entity);
    }

    public async Task<HabitacionDto> CreateAsync(HabitacionWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (await _repository.ExisteNumeroAsync(dto.Numero, null, cancellationToken))
        {
            throw new BusinessException($"Ya existe una habitación con el número '{dto.Numero}'.");
        }

        var entity = new Habitacion
        {
            Numero = dto.Numero?.Trim() ?? string.Empty,
            Piso = dto.Piso,
            Tipo = dto.Tipo?.Trim() ?? string.Empty,
            Disponible = dto.Disponible
        };

        await _repository.AddAsync(entity, cancellationToken);
        return Map(entity);
    }

    public async Task<HabitacionDto?> UpdateAsync(Guid id, HabitacionWriteDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        if (await _repository.ExisteNumeroAsync(dto.Numero, id, cancellationToken))
        {
            throw new BusinessException($"Ya existe una habitación con el número '{dto.Numero}'.");
        }

        entity.Numero = dto.Numero?.Trim() ?? string.Empty;
        entity.Piso = dto.Piso;
        entity.Tipo = dto.Tipo?.Trim() ?? string.Empty;
        entity.Disponible = dto.Disponible;

        await _repository.UpdateAsync(entity, cancellationToken);
        return Map(entity);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);

    private static HabitacionDto Map(Habitacion e) => new(e.Id, e.Numero, e.Piso, e.Tipo, e.Disponible);
}
