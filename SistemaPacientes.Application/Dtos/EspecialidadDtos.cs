namespace SistemaPacientes.Application.Dtos;

public sealed record EspecialidadDto(Guid Id, string Codigo, string Nombre, string Descripcion);

public sealed record EspecialidadWriteDto(string Codigo, string Nombre, string Descripcion);
