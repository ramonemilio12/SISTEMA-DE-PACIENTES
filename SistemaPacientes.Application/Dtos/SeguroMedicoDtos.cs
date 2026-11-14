namespace SistemaPacientes.Application.Dtos;

public sealed record SeguroMedicoDto(Guid Id, string Codigo, string Nombre, decimal Cobertura, string Telefono);

public sealed record SeguroMedicoWriteDto(string Codigo, string Nombre, decimal Cobertura, string Telefono);
