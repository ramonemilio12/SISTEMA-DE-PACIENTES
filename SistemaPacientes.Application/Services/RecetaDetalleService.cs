using SistemaPacientes.Application.Common;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public sealed class RecetaDetalleService : IRecetaDetalleService
{
    private readonly IRepository<RecetaDetalle> _repository;
    private readonly IRepository<Receta> _recetaRepository;
    private readonly IRepository<Medicamento> _medicamentoRepository;

    public RecetaDetalleService(
        IRepository<RecetaDetalle> repository,
        IRepository<Receta> recetaRepository,
        IRepository<Medicamento> medicamentoRepository)
    {
        _repository = repository;
        _recetaRepository = recetaRepository;
        _medicamentoRepository = medicamentoRepository;
    }

    public async Task<IReadOnlyList<RecetaDetalleDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(Map).ToList();
    }

    public async Task<RecetaDetalleDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : Map(entity);
    }

    public async Task<RecetaDetalleDto> CreateAsync(RecetaDetalleWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (!await _recetaRepository.ExistsAsync(dto.RecetaId, cancellationToken))
        {
            throw new BusinessException("La receta indicada no existe.");
        }

        if (!await _medicamentoRepository.ExistsAsync(dto.MedicamentoId, cancellationToken))
        {
            throw new BusinessException("El medicamento indicado no existe.");
        }

        var entity = new RecetaDetalle
        {
            RecetaId = dto.RecetaId,
            MedicamentoId = dto.MedicamentoId,
            Dosis = dto.Dosis?.Trim() ?? string.Empty,
            Frecuencia = dto.Frecuencia?.Trim() ?? string.Empty,
            DuracionDias = dto.DuracionDias
        };

        await _repository.AddAsync(entity, cancellationToken);
        return Map(entity);
    }

    public async Task<RecetaDetalleDto?> UpdateAsync(Guid id, RecetaDetalleWriteDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        if (!await _recetaRepository.ExistsAsync(dto.RecetaId, cancellationToken))
        {
            throw new BusinessException("La receta indicada no existe.");
        }

        if (!await _medicamentoRepository.ExistsAsync(dto.MedicamentoId, cancellationToken))
        {
            throw new BusinessException("El medicamento indicado no existe.");
        }

        entity.RecetaId = dto.RecetaId;
        entity.MedicamentoId = dto.MedicamentoId;
        entity.Dosis = dto.Dosis?.Trim() ?? string.Empty;
        entity.Frecuencia = dto.Frecuencia?.Trim() ?? string.Empty;
        entity.DuracionDias = dto.DuracionDias;

        await _repository.UpdateAsync(entity, cancellationToken);
        return Map(entity);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);

    private static RecetaDetalleDto Map(RecetaDetalle e) => new(e.Id, e.RecetaId, e.MedicamentoId, e.Dosis, e.Frecuencia, e.DuracionDias);
}
