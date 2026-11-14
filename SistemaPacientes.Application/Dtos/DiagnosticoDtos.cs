namespace SistemaPacientes.Application.Dtos;

public sealed record DiagnosticoDto(Guid Id, Guid ConsultaId, string Codigo, string Descripcion, string Gravedad);

public sealed record DiagnosticoWriteDto(Guid ConsultaId, string Codigo, string Descripcion, string Gravedad);
