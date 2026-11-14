using SistemaPacientes.Application.Common;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public sealed class MedicamentoService : IMedicamentoService
{
    private readonly IMedicamentoRepository _repository;

    public MedicamentoService(IMedicamentoRepository repository) => _repository = repository;

    public async Task<IReadOnlyList<MedicamentoDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(Map).ToList();
    }

    public async Task<MedicamentoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : Map(entity);
    }

    public async Task<MedicamentoDto> CreateAsync(MedicamentoWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (await _repository.ExisteCodigoAsync(dto.Codigo, null, cancellationToken))
        {
            throw new BusinessException($"Ya existe un medicamento con el código '{dto.Codigo}'.");
        }

        var entity = new Medicamento
        {
            Codigo = dto.Codigo?.Trim() ?? string.Empty,
            Nombre = dto.Nombre?.Trim() ?? string.Empty,
            Presentacion = dto.Presentacion?.Trim() ?? string.Empty,
            Concentracion = dto.Concentracion?.Trim() ?? string.Empty
        };

        await _repository.AddAsync(entity, cancellationToken);
        return Map(entity);
    }

    public async Task<MedicamentoDto?> UpdateAsync(Guid id, MedicamentoWriteDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        if (await _repository.ExisteCodigoAsync(dto.Codigo, id, cancellationToken))
        {
            throw new BusinessException($"Ya existe un medicamento con el código '{dto.Codigo}'.");
        }

        entity.Codigo = dto.Codigo?.Trim() ?? string.Empty;
        entity.Nombre = dto.Nombre?.Trim() ?? string.Empty;
        entity.Presentacion = dto.Presentacion?.Trim() ?? string.Empty;
        entity.Concentracion = dto.Concentracion?.Trim() ?? string.Empty;

        await _repository.UpdateAsync(entity, cancellationToken);
        return Map(entity);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);

    private static MedicamentoDto Map(Medicamento e) => new(e.Id, e.Codigo, e.Nombre, e.Presentacion, e.Concentracion);
}
