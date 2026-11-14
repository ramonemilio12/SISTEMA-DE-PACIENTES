namespace SistemaPacientes.Application.Dtos;

public sealed record HabitacionDto(Guid Id, string Numero, int Piso, string Tipo, bool Disponible);

public sealed record HabitacionWriteDto(string Numero, int Piso, string Tipo, bool Disponible);
