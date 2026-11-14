using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities;

public sealed class RecetaDetalle : BaseEntity
{
    public Guid RecetaId { get; set; }
    public Guid MedicamentoId { get; set; }
    public string Dosis { get; set; } = string.Empty;
    public string Frecuencia { get; set; } = string.Empty;
    public int DuracionDias { get; set; }

    public Receta Receta { get; set; } = null!;
    public Medicamento Medicamento { get; set; } = null!;
}
