using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities;

public sealed class Medicamento : BaseEntity
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Presentacion { get; set; } = string.Empty;
    public string Concentracion { get; set; } = string.Empty;

    public ICollection<RecetaDetalle> RecetaDetalles { get; set; } = new List<RecetaDetalle>();
}
