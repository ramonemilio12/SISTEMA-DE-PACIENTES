using SistemaPacientes.Application.Common;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;
using SistemaPacientes.Domain.Entities;
using SistemaPacientes.Domain.Interfaces;

namespace SistemaPacientes.Application.Services;

public sealed class PacienteService : IPacienteService
{
    private readonly IPacienteRepository _repository;
    private readonly IRepository<SeguroMedico> _seguroRepository;

    public PacienteService(IPacienteRepository repository, IRepository<SeguroMedico> seguroRepository)
    {
        _repository = repository;
        _seguroRepository = seguroRepository;
    }

    public async Task<IReadOnlyList<PacienteDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(Map).ToList();
    }

    public async Task<PacienteDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : Map(entity);
    }

    public async Task<PacienteDto> CreateAsync(PacienteWriteDto dto, CancellationToken cancellationToken = default)
    {
        if (await _repository.ExisteExpedienteAsync(dto.NumeroExpediente, null, cancellationToken))
        {
            throw new BusinessException($"Ya existe un paciente con el número de expediente '{dto.NumeroExpediente}'.");
        }

        if (await _repository.ExisteDocumentoAsync(dto.Documento, null, cancellationToken))
        {
            throw new BusinessException($"Ya existe un paciente con el documento '{dto.Documento}'.");
        }

        if (dto.SeguroMedicoId.HasValue && !await _seguroRepository.ExistsAsync(dto.SeguroMedicoId.Value, cancellationToken))
        {
            throw new BusinessException("El seguro médico indicado no existe.");
        }

        var entity = new Paciente
        {
            NumeroExpediente = dto.NumeroExpediente?.Trim() ?? string.Empty,
            Nombre = dto.Nombre?.Trim() ?? string.Empty,
            Apellido = dto.Apellido?.Trim() ?? string.Empty,
            Documento = dto.Documento?.Trim() ?? string.Empty,
            FechaNacimiento = dto.FechaNacimiento,
            Sexo = dto.Sexo?.Trim() ?? string.Empty,
            Telefono = dto.Telefono?.Trim() ?? string.Empty,
            Correo = dto.Correo?.Trim() ?? string.Empty,
            Direccion = dto.Direccion?.Trim() ?? string.Empty,
            TipoSangre = dto.TipoSangre?.Trim() ?? string.Empty,
            SeguroMedicoId = dto.SeguroMedicoId
        };

        await _repository.AddAsync(entity, cancellationToken);
        return Map(entity);
    }

    public async Task<PacienteDto?> UpdateAsync(Guid id, PacienteWriteDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        if (await _repository.ExisteExpedienteAsync(dto.NumeroExpediente, id, cancellationToken))
        {
            throw new BusinessException($"Ya existe un paciente con el número de expediente '{dto.NumeroExpediente}'.");
        }

        if (await _repository.ExisteDocumentoAsync(dto.Documento, id, cancellationToken))
        {
            throw new BusinessException($"Ya existe un paciente con el documento '{dto.Documento}'.");
        }

        if (dto.SeguroMedicoId.HasValue && !await _seguroRepository.ExistsAsync(dto.SeguroMedicoId.Value, cancellationToken))
        {
            throw new BusinessException("El seguro médico indicado no existe.");
        }

        entity.NumeroExpediente = dto.NumeroExpediente?.Trim() ?? string.Empty;
        entity.Nombre = dto.Nombre?.Trim() ?? string.Empty;
        entity.Apellido = dto.Apellido?.Trim() ?? string.Empty;
        entity.Documento = dto.Documento?.Trim() ?? string.Empty;
        entity.FechaNacimiento = dto.FechaNacimiento;
        entity.Sexo = dto.Sexo?.Trim() ?? string.Empty;
        entity.Telefono = dto.Telefono?.Trim() ?? string.Empty;
        entity.Correo = dto.Correo?.Trim() ?? string.Empty;
        entity.Direccion = dto.Direccion?.Trim() ?? string.Empty;
        entity.TipoSangre = dto.TipoSangre?.Trim() ?? string.Empty;
        entity.SeguroMedicoId = dto.SeguroMedicoId;

        await _repository.UpdateAsync(entity, cancellationToken);
        return Map(entity);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => _repository.DeleteAsync(id, cancellationToken);

    private static PacienteDto Map(Paciente e) => new(
        e.Id,
        e.NumeroExpediente,
        e.Nombre,
        e.Apellido,
        e.Documento,
        e.FechaNacimiento,
        e.Sexo,
        e.Telefono,
        e.Correo,
        e.Direccion,
        e.TipoSangre,
        e.SeguroMedicoId);
}
