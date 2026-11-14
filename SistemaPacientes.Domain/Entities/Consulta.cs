using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities;

public sealed class Consulta : BaseEntity
{
    public Guid? CitaId { get; set; }
    public Guid PacienteId { get; set; }
    public Guid MedicoId { get; set; }
    public DateTime FechaHora { get; set; } = DateTime.UtcNow;
    public string Sintomas { get; set; } = string.Empty;
    public string Observaciones { get; set; } = string.Empty;

    public Cita? Cita { get; set; }
    public Paciente Paciente { get; set; } = null!;
    public Medico Medico { get; set; } = null!;
    public ICollection<Diagnostico> Diagnosticos { get; set; } = new List<Diagnostico>();
    public ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
    public ICollection<Receta> Recetas { get; set; } = new List<Receta>();
}
