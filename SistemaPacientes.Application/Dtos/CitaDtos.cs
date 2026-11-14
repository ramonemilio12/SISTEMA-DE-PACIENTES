namespace SistemaPacientes.Application.Dtos;

public sealed record CitaDto(Guid Id, Guid PacienteId, Guid MedicoId, DateTime FechaHora, string Motivo, string Estado);

public sealed record CitaWriteDto(Guid PacienteId, Guid MedicoId, DateTime FechaHora, string Motivo, string Estado);
