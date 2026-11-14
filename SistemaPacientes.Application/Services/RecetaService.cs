using SistemaPacientes.Application.Common;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public sealed class RecetaService : IRecetaService
{
    private readonly IRepository<Receta> _repository;
    private readonly IRepository<Consulta> _consultaRepository;
    private readonly IRepository<Medico> _medicoRepository;
    private readonly IRepository<Paciente> _pacienteRepository;

    public RecetaService(
        IRepository<Receta> repository,
        IRepository<Consulta> consultaRepository,
        IRepository<Medico> medicoRepository,
        IRepository<Paciente> pacienteRepository)
    {
        _repository = repository;
        _consultaRepository = consultaRepository;
        _medicoRepository = medicoRepository;
        _pacienteRepository = pacienteRepository;
    }

    public async Task<IReadOnlyList<RecetaDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(Map).ToList();
    }

    public async Task<RecetaDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : Map(entity);
    }

    public async Task<RecetaDto> CreateAsync(RecetaWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (!await _consultaRepository.ExistsAsync(dto.ConsultaId, cancellationToken))
        {
            throw new BusinessException("La consulta indicada no existe.");
        }

        if (!await _medicoRepository.ExistsAsync(dto.MedicoId, cancellationToken))
        {
            throw new BusinessException("El médico indicado no existe.");
        }

        if (!await _pacienteRepository.ExistsAsync(dto.PacienteId, cancellationToken))
        {
            throw new BusinessException("El paciente indicado no existe.");
        }

        var entity = new Receta
        {
            ConsultaId = dto.ConsultaId,
            MedicoId = dto.MedicoId,
            PacienteId = dto.PacienteId,
            FechaEmision = dto.FechaEmision,
            Indicaciones = dto.Indicaciones?.Trim() ?? string.Empty
        };

        await _repository.AddAsync(entity, cancellationToken);
        return Map(entity);
    }

    public async Task<RecetaDto?> UpdateAsync(Guid id, RecetaWriteDto dto, CancellationToken cancellationToken = default)
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

        if (!await _medicoRepository.ExistsAsync(dto.MedicoId, cancellationToken))
        {
            throw new BusinessException("El médico indicado no existe.");
        }

        if (!await _pacienteRepository.ExistsAsync(dto.PacienteId, cancellationToken))
        {
            throw new BusinessException("El paciente indicado no existe.");
        }

        entity.ConsultaId = dto.ConsultaId;
        entity.MedicoId = dto.MedicoId;
        entity.PacienteId = dto.PacienteId;
        entity.FechaEmision = dto.FechaEmision;
        entity.Indicaciones = dto.Indicaciones?.Trim() ?? string.Empty;

        await _repository.UpdateAsync(entity, cancellationToken);
        return Map(entity);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);

    private static RecetaDto Map(Receta e) => new(e.Id, e.ConsultaId, e.MedicoId, e.PacienteId, e.FechaEmision, e.Indicaciones);
}
