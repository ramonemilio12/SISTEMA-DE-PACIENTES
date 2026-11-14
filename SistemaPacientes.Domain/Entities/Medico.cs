using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities;

public sealed class Medico : BaseEntity
{
    public string NumeroColegiado { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public Guid EspecialidadId { get; set; }

    public Especialidad Especialidad { get; set; } = null!;
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
    public ICollection<Receta> Recetas { get; set; } = new List<Receta>();

    public string NombreCompleto => $"{Nombre} {Apellido}".Trim();
}
