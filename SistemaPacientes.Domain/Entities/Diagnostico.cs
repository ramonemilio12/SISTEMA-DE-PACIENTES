using SistemaPacientes.Domain.Core;

namespace SistemaPacientes.Domain.Entities;

public sealed class Diagnostico : BaseEntity
{
    public Guid ConsultaId { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Gravedad { get; set; } = string.Empty;

    public Consulta Consulta { get; set; } = null!;
}
