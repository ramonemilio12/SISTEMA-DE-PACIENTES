using SistemaPacientes.Application.Common;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public sealed class ConsultaService : IConsultaService
{
    private readonly IRepository<Consulta> _repository;
    private readonly IRepository<Cita> _citaRepository;
    private readonly IRepository<Paciente> _pacienteRepository;
    private readonly IRepository<Medico> _medicoRepository;

    public ConsultaService(
        IRepository<Consulta> repository,
        IRepository<Cita> citaRepository,
        IRepository<Paciente> pacienteRepository,
        IRepository<Medico> medicoRepository)
    {
        _repository = repository;
        _citaRepository = citaRepository;
        _pacienteRepository = pacienteRepository;
        _medicoRepository = medicoRepository;
    }

    public async Task<IReadOnlyList<ConsultaDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(Map).ToList();
    }

    public async Task<ConsultaDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : Map(entity);
    }

    public async Task<ConsultaDto> CreateAsync(ConsultaWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (dto.CitaId.HasValue && !await _citaRepository.ExistsAsync(dto.CitaId.Value, cancellationToken))
        {
            throw new BusinessException("La cita indicada no existe.");
        }

        if (!await _pacienteRepository.ExistsAsync(dto.PacienteId, cancellationToken))
        {
            throw new BusinessException("El paciente indicado no existe.");
        }

        if (!await _medicoRepository.ExistsAsync(dto.MedicoId, cancellationToken))
        {
            throw new BusinessException("El médico indicado no existe.");
        }

        var entity = new Consulta
        {
            CitaId = dto.CitaId,
            PacienteId = dto.PacienteId,
            MedicoId = dto.MedicoId,
            FechaHora = dto.FechaHora,
            Sintomas = dto.Sintomas?.Trim() ?? string.Empty,
            Observaciones = dto.Observaciones?.Trim() ?? string.Empty
        };

        await _repository.AddAsync(entity, cancellationToken);
        return Map(entity);
    }

    public async Task<ConsultaDto?> UpdateAsync(Guid id, ConsultaWriteDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        if (dto.CitaId.HasValue && !await _citaRepository.ExistsAsync(dto.CitaId.Value, cancellationToken))
        {
            throw new BusinessException("La cita indicada no existe.");
        }

        if (!await _pacienteRepository.ExistsAsync(dto.PacienteId, cancellationToken))
        {
            throw new BusinessException("El paciente indicado no existe.");
        }

        if (!await _medicoRepository.ExistsAsync(dto.MedicoId, cancellationToken))
        {
            throw new BusinessException("El médico indicado no existe.");
        }

        entity.CitaId = dto.CitaId;
        entity.PacienteId = dto.PacienteId;
        entity.MedicoId = dto.MedicoId;
        entity.FechaHora = dto.FechaHora;
        entity.Sintomas = dto.Sintomas?.Trim() ?? string.Empty;
        entity.Observaciones = dto.Observaciones?.Trim() ?? string.Empty;

        await _repository.UpdateAsync(entity, cancellationToken);
        return Map(entity);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);

    private static ConsultaDto Map(Consulta e) => new(e.Id, e.CitaId, e.PacienteId, e.MedicoId, e.FechaHora, e.Sintomas, e.Observaciones);
}
