using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities;

public sealed class SeguroMedico : BaseEntity
{
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public decimal Cobertura { get; set; }
    public string Telefono { get; set; } = string.Empty;

    public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
}
