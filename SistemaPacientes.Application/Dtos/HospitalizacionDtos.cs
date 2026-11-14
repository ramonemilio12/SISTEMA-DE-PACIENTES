namespace SistemaPacientes.Application.Dtos;

public sealed record HospitalizacionDto(Guid Id, Guid PacienteId, Guid HabitacionId, DateTime FechaIngreso, DateTime? FechaAlta, string Motivo);

public sealed record HospitalizacionWriteDto(Guid PacienteId, Guid HabitacionId, DateTime FechaIngreso, DateTime? FechaAlta, string Motivo);
