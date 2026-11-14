using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities;

public sealed class Cita : BaseEntity
{
    public Guid PacienteId { get; set; }
    public Guid MedicoId { get; set; }
    public DateTime FechaHora { get; set; } = DateTime.UtcNow;
    public string Motivo { get; set; } = string.Empty;
    public string Estado { get; set; } = "Programada";

    public Paciente Paciente { get; set; } = null!;
    public Medico Medico { get; set; } = null!;
}
