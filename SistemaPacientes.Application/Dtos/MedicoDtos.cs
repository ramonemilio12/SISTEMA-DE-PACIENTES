namespace SistemaPacientes.Application.Dtos;

public sealed record MedicoDto(Guid Id, string NumeroColegiado, string Nombre, string Apellido, string Correo, string Telefono, Guid EspecialidadId);

public sealed record MedicoWriteDto(string NumeroColegiado, string Nombre, string Apellido, string Correo, string Telefono, Guid EspecialidadId);
