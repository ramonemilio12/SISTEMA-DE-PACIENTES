namespace SistemaPacientes.Application.Dtos;

public sealed record PacienteDto(Guid Id, string NumeroExpediente, string Nombre, string Apellido, string Documento, DateTime FechaNacimiento, string Sexo, string Telefono, string Correo, string Direccion, string TipoSangre, Guid? SeguroMedicoId);

public sealed record PacienteWriteDto(string NumeroExpediente, string Nombre, string Apellido, string Documento, DateTime FechaNacimiento, string Sexo, string Telefono, string Correo, string Direccion, string TipoSangre, Guid? SeguroMedicoId);
