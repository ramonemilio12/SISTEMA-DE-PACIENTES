using SistemaPacientes.Application.Common;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public sealed class SeguroMedicoService : ISeguroMedicoService
{
    private readonly ISeguroMedicoRepository _repository;

    public SeguroMedicoService(ISeguroMedicoRepository repository) => _repository = repository;

    public async Task<IReadOnlyList<SeguroMedicoDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(Map).ToList();
    }

    public async Task<SeguroMedicoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : Map(entity);
    }

    public async Task<SeguroMedicoDto> CreateAsync(SeguroMedicoWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (await _repository.ExisteCodigoAsync(dto.Codigo, null, cancellationToken))
        {
            throw new BusinessException($"Ya existe un seguro médico con el código '{dto.Codigo}'.");
        }

        var entity = new SeguroMedico
        {
            Codigo = dto.Codigo?.Trim() ?? string.Empty,
            Nombre = dto.Nombre?.Trim() ?? string.Empty,
            Cobertura = dto.Cobertura,
            Telefono = dto.Telefono?.Trim() ?? string.Empty
        };

        await _repository.AddAsync(entity, cancellationToken);
        return Map(entity);
    }

    public async Task<SeguroMedicoDto?> UpdateAsync(Guid id, SeguroMedicoWriteDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        if (await _repository.ExisteCodigoAsync(dto.Codigo, id, cancellationToken))
        {
            throw new BusinessException($"Ya existe un seguro médico con el código '{dto.Codigo}'.");
        }

        entity.Codigo = dto.Codigo?.Trim() ?? string.Empty;
        entity.Nombre = dto.Nombre?.Trim() ?? string.Empty;
        entity.Cobertura = dto.Cobertura;
        entity.Telefono = dto.Telefono?.Trim() ?? string.Empty;

        await _repository.UpdateAsync(entity, cancellationToken);
        return Map(entity);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);

    private static SeguroMedicoDto Map(SeguroMedico e) => new(e.Id, e.Codigo, e.Nombre, e.Cobertura, e.Telefono);
}
