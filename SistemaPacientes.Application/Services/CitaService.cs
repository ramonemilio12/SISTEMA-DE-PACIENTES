using SistemaPacientes.Application.Common;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public sealed class CitaService : ICitaService
{
    private readonly ICitaRepository _repository;
    private readonly IRepository<Paciente> _pacienteRepository;
    private readonly IRepository<Medico> _medicoRepository;

    public CitaService(ICitaRepository repository, IRepository<Paciente> pacienteRepository, IRepository<Medico> medicoRepository)
    {
        _repository = repository;
        _pacienteRepository = pacienteRepository;
        _medicoRepository = medicoRepository;
    }

    public async Task<IReadOnlyList<CitaDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(Map).ToList();
    }

    public async Task<CitaDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : Map(entity);
    }

    public async Task<CitaDto> CreateAsync(CitaWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (!await _pacienteRepository.ExistsAsync(dto.PacienteId, cancellationToken))
        {
            throw new BusinessException("El paciente indicado no existe.");
        }

        if (!await _medicoRepository.ExistsAsync(dto.MedicoId, cancellationToken))
        {
            throw new BusinessException("El médico indicado no existe.");
        }

        var entity = new Cita
        {
            PacienteId = dto.PacienteId,
            MedicoId = dto.MedicoId,
            FechaHora = dto.FechaHora,
            Motivo = dto.Motivo?.Trim() ?? string.Empty,
            Estado = string.IsNullOrWhiteSpace(dto.Estado) ? "Programada" : dto.Estado.Trim()
        };

        await _repository.AddAsync(entity, cancellationToken);
        return Map(entity);
    }

    public async Task<CitaDto?> UpdateAsync(Guid id, CitaWriteDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        if (!await _pacienteRepository.ExistsAsync(dto.PacienteId, cancellationToken))
        {
            throw new BusinessException("El paciente indicado no existe.");
        }

        if (!await _medicoRepository.ExistsAsync(dto.MedicoId, cancellationToken))
        {
            throw new BusinessException("El médico indicado no existe.");
        }

        entity.PacienteId = dto.PacienteId;
        entity.MedicoId = dto.MedicoId;
        entity.FechaHora = dto.FechaHora;
        entity.Motivo = dto.Motivo?.Trim() ?? string.Empty;
        entity.Estado = string.IsNullOrWhiteSpace(dto.Estado) ? "Programada" : dto.Estado.Trim();

        await _repository.UpdateAsync(entity, cancellationToken);
        return Map(entity);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);

    private static CitaDto Map(Cita e) => new(e.Id, e.PacienteId, e.MedicoId, e.FechaHora, e.Motivo, e.Estado);
}
