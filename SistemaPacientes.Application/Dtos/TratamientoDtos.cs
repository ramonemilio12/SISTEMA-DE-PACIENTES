namespace SistemaPacientes.Application.Dtos;

public sealed record TratamientoDto(Guid Id, Guid ConsultaId, string Descripcion, DateTime FechaInicio, DateTime FechaFin, string Indicaciones);

public sealed record TratamientoWriteDto(Guid ConsultaId, string Descripcion, DateTime FechaInicio, DateTime FechaFin, string Indicaciones);
