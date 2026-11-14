using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities;

public sealed class Receta : BaseEntity
{
    public Guid ConsultaId { get; set; }
    public Guid MedicoId { get; set; }
    public Guid PacienteId { get; set; }
    public DateTime FechaEmision { get; set; } = DateTime.UtcNow;
    public string Indicaciones { get; set; } = string.Empty;

    public Consulta Consulta { get; set; } = null!;
    public Medico Medico { get; set; } = null!;
    public Paciente Paciente { get; set; } = null!;
    public ICollection<RecetaDetalle> Detalles { get; set; } = new List<RecetaDetalle>();
}
