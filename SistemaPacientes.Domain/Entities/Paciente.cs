using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities;

public sealed class Paciente : BaseEntity
{
    public string NumeroExpediente { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public string Sexo { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string TipoSangre { get; set; } = string.Empty;
    public Guid? SeguroMedicoId { get; set; }

    public SeguroMedico? SeguroMedico { get; set; }
    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
    public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
    public ICollection<Receta> Recetas { get; set; } = new List<Receta>();
    public ICollection<Hospitalizacion> Hospitalizaciones { get; set; } = new List<Hospitalizacion>();

    public string NombreCompleto => $"{Nombre} {Apellido}".Trim();
}
