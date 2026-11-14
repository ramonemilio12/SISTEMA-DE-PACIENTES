namespace SistemaPacientes.Application.Dtos;

public sealed record RecetaDetalleDto(Guid Id, Guid RecetaId, Guid MedicamentoId, string Dosis, string Frecuencia, int DuracionDias);

public sealed record RecetaDetalleWriteDto(Guid RecetaId, Guid MedicamentoId, string Dosis, string Frecuencia, int DuracionDias);
