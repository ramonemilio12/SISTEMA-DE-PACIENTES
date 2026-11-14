using SistemaPacientes.Application.Common;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public sealed class HospitalizacionService : IHospitalizacionService
{
    private readonly IRepository<Hospitalizacion> _repository;
    private readonly IRepository<Paciente> _pacienteRepository;
    private readonly IRepository<Habitacion> _habitacionRepository;

    public HospitalizacionService(
        IRepository<Hospitalizacion> repository,
        IRepository<Paciente> pacienteRepository,
        IRepository<Habitacion> habitacionRepository)
    {
        _repository = repository;
        _pacienteRepository = pacienteRepository;
        _habitacionRepository = habitacionRepository;
    }

    public async Task<IReadOnlyList<HospitalizacionDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(Map).ToList();
    }

    public async Task<HospitalizacionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : Map(entity);
    }

    public async Task<HospitalizacionDto> CreateAsync(HospitalizacionWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (!await _pacienteRepository.ExistsAsync(dto.PacienteId, cancellationToken))
        {
            throw new BusinessException("El paciente indicado no existe.");
        }

        if (!await _habitacionRepository.ExistsAsync(dto.HabitacionId, cancellationToken))
        {
            throw new BusinessException("La habitación indicada no existe.");
        }

        var entity = new Hospitalizacion
        {
            PacienteId = dto.PacienteId,
            HabitacionId = dto.HabitacionId,
            FechaIngreso = dto.FechaIngreso,
            FechaAlta = dto.FechaAlta,
            Motivo = dto.Motivo?.Trim() ?? string.Empty
        };

        await _repository.AddAsync(entity, cancellationToken);
        return Map(entity);
    }

    public async Task<HospitalizacionDto?> UpdateAsync(Guid id, HospitalizacionWriteDto dto, CancellationToken cancellationToken = default)
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

        if (!await _habitacionRepository.ExistsAsync(dto.HabitacionId, cancellationToken))
        {
            throw new BusinessException("La habitación indicada no existe.");
        }

        entity.PacienteId = dto.PacienteId;
        entity.HabitacionId = dto.HabitacionId;
        entity.FechaIngreso = dto.FechaIngreso;
        entity.FechaAlta = dto.FechaAlta;
        entity.Motivo = dto.Motivo?.Trim() ?? string.Empty;

        await _repository.UpdateAsync(entity, cancellationToken);
        return Map(entity);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);

    private static HospitalizacionDto Map(Hospitalizacion e) => new(e.Id, e.PacienteId, e.HabitacionId, e.FechaIngreso, e.FechaAlta, e.Motivo);
}
