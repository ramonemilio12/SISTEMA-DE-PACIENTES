namespace SistemaPacientes.Application.Dtos;

public sealed record RecetaDto(Guid Id, Guid ConsultaId, Guid MedicoId, Guid PacienteId, DateTime FechaEmision, string Indicaciones);

public sealed record RecetaWriteDto(Guid ConsultaId, Guid MedicoId, Guid PacienteId, DateTime FechaEmision, string Indicaciones);
