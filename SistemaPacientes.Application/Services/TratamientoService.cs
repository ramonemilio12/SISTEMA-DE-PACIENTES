using SistemaPacientes.Application.Common;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public sealed class TratamientoService : ITratamientoService
{
    private readonly IRepository<Tratamiento> _repository;
    private readonly IRepository<Consulta> _consultaRepository;

    public TratamientoService(IRepository<Tratamiento> repository, IRepository<Consulta> consultaRepository)
    {
        _repository = repository;
        _consultaRepository = consultaRepository;
    }

    public async Task<IReadOnlyList<TratamientoDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(Map).ToList();
    }

    public async Task<TratamientoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : Map(entity);
    }

    public async Task<TratamientoDto> CreateAsync(TratamientoWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (!await _consultaRepository.ExistsAsync(dto.ConsultaId, cancellationToken))
        {
            throw new BusinessException("La consulta indicada no existe.");
        }

        var entity = new Tratamiento
        {
            ConsultaId = dto.ConsultaId,
            Descripcion = dto.Descripcion?.Trim() ?? string.Empty,
            FechaInicio = dto.FechaInicio,
            FechaFin = dto.FechaFin,
            Indicaciones = dto.Indicaciones?.Trim() ?? string.Empty
        };

        await _repository.AddAsync(entity, cancellationToken);
        return Map(entity);
    }

    public async Task<TratamientoDto?> UpdateAsync(Guid id, TratamientoWriteDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        if (!await _consultaRepository.ExistsAsync(dto.ConsultaId, cancellationToken))
        {
            throw new BusinessException("La consulta indicada no existe.");
        }

        entity.ConsultaId = dto.ConsultaId;
        entity.Descripcion = dto.Descripcion?.Trim() ?? string.Empty;
        entity.FechaInicio = dto.FechaInicio;
        entity.FechaFin = dto.FechaFin;
        entity.Indicaciones = dto.Indicaciones?.Trim() ?? string.Empty;

        await _repository.UpdateAsync(entity, cancellationToken);
        return Map(entity);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);

    private static TratamientoDto Map(Tratamiento e) => new(e.Id, e.ConsultaId, e.Descripcion, e.FechaInicio, e.FechaFin, e.Indicaciones);
}
