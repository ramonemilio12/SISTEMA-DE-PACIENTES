using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities;

public sealed class Tratamiento : BaseEntity
{
    public Guid ConsultaId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; } = DateTime.UtcNow;
    public DateTime FechaFin { get; set; } = DateTime.UtcNow;
    public string Indicaciones { get; set; } = string.Empty;

    public Consulta Consulta { get; set; } = null!;
}
