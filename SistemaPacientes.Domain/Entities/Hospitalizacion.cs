using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities;

public sealed class Hospitalizacion : BaseEntity
{
    public Guid PacienteId { get; set; }
    public Guid HabitacionId { get; set; }
    public DateTime FechaIngreso { get; set; } = DateTime.UtcNow;
    public DateTime? FechaAlta { get; set; }
    public string Motivo { get; set; } = string.Empty;

    public Paciente Paciente { get; set; } = null!;
    public Habitacion Habitacion { get; set; } = null!;
}
