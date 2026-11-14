namespace SistemaPacientes.Application.Dtos;

public sealed record MedicamentoDto(Guid Id, string Codigo, string Nombre, string Presentacion, string Concentracion);

public sealed record MedicamentoWriteDto(string Codigo, string Nombre, string Presentacion, string Concentracion);
