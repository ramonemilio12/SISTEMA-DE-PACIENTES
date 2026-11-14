using SistemaPacientes.Application.Common;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public sealed class DiagnosticoService : IDiagnosticoService
{
    private readonly IRepository<Diagnostico> _repository;
    private readonly IRepository<Consulta> _consultaRepository;

    public DiagnosticoService(IRepository<Diagnostico> repository, IRepository<Consulta> consultaRepository)
    {
        _repository = repository;
        _consultaRepository = consultaRepository;
    }

    public async Task<IReadOnlyList<DiagnosticoDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(Map).ToList();
    }

    public async Task<DiagnosticoDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : Map(entity);
    }

    public async Task<DiagnosticoDto> CreateAsync(DiagnosticoWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (!await _consultaRepository.ExistsAsync(dto.ConsultaId, cancellationToken))
        {
            throw new BusinessException("La consulta indicada no existe.");
        }

        var entity = new Diagnostico
        {
            ConsultaId = dto.ConsultaId,
            Codigo = dto.Codigo?.Trim() ?? string.Empty,
            Descripcion = dto.Descripcion?.Trim() ?? string.Empty,
            Gravedad = dto.Gravedad?.Trim() ?? string.Empty
        };

        await _repository.AddAsync(entity, cancellationToken);
        return Map(entity);
    }

    public async Task<DiagnosticoDto?> UpdateAsync(Guid id, DiagnosticoWriteDto dto, CancellationToken cancellationToken = default)
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
        entity.Codigo = dto.Codigo?.Trim() ?? string.Empty;
        entity.Descripcion = dto.Descripcion?.Trim() ?? string.Empty;
        entity.Gravedad = dto.Gravedad?.Trim() ?? string.Empty;

        await _repository.UpdateAsync(entity, cancellationToken);
        return Map(entity);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);

    private static DiagnosticoDto Map(Diagnostico e) => new(e.Id, e.ConsultaId, e.Codigo, e.Descripcion, e.Gravedad);
}
