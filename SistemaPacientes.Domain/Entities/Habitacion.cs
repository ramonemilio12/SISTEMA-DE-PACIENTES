using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities;

public sealed class Habitacion : BaseEntity
{
    public string Numero { get; set; } = string.Empty;
    public int Piso { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public bool Disponible { get; set; } = true;

    public ICollection<Hospitalizacion> Hospitalizaciones { get; set; } = new List<Hospitalizacion>();
}
