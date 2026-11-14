namespace SistemaPacientes.Application.Dtos;

public sealed record ConsultaDto(Guid Id, Guid? CitaId, Guid PacienteId, Guid MedicoId, DateTime FechaHora, string Sintomas, string Observaciones);

public sealed record ConsultaWriteDto(Guid? CitaId, Guid PacienteId, Guid MedicoId, DateTime FechaHora, string Sintomas, string Observaciones);
