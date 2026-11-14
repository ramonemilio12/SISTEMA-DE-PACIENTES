namespace SistemaPacientes.Blazor.Models;

public sealed class EspecialidadModel
{
    public Guid Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}

public sealed class SeguroMedicoModel
{
    public Guid Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public decimal Cobertura { get; set; }
    public string Telefono { get; set; } = string.Empty;
}

public sealed class HabitacionModel
{
    public Guid Id { get; set; }
    public string Numero { get; set; } = string.Empty;
    public int Piso { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public bool Disponible { get; set; } = true;
}

public sealed class MedicamentoModel
{
    public Guid Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Presentacion { get; set; } = string.Empty;
    public string Concentracion { get; set; } = string.Empty;
}

public sealed class PacienteModel
{
    public Guid Id { get; set; }
    public string NumeroExpediente { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; } = DateTime.Today;
    public string Sexo { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string TipoSangre { get; set; } = string.Empty;
    public Guid? SeguroMedicoId { get; set; }
}

public sealed class MedicoModel
{
    public Guid Id { get; set; }
    public string NumeroColegiado { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public Guid EspecialidadId { get; set; }
}

public sealed class CitaModel
{
    public Guid Id { get; set; }
    public Guid PacienteId { get; set; }
    public Guid MedicoId { get; set; }
    public DateTime FechaHora { get; set; } = DateTime.Now;
    public string Motivo { get; set; } = string.Empty;
    public string Estado { get; set; } = "Programada";
}

public sealed class ConsultaModel
{
    public Guid Id { get; set; }
    public Guid? CitaId { get; set; }
    public Guid PacienteId { get; set; }
    public Guid MedicoId { get; set; }
    public DateTime FechaHora { get; set; } = DateTime.Now;
    public string Sintomas { get; set; } = string.Empty;
    public string Observaciones { get; set; } = string.Empty;
}

public sealed class DiagnosticoModel
{
    public Guid Id { get; set; }
    public Guid ConsultaId { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public string Gravedad { get; set; } = string.Empty;
}

public sealed class TratamientoModel
{
    public Guid Id { get; set; }
    public Guid ConsultaId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; } = DateTime.Today;
    public DateTime FechaFin { get; set; } = DateTime.Today;
    public string Indicaciones { get; set; } = string.Empty;
}

public sealed class RecetaModel
{
    public Guid Id { get; set; }
    public Guid ConsultaId { get; set; }
    public Guid MedicoId { get; set; }
    public Guid PacienteId { get; set; }
    public DateTime FechaEmision { get; set; } = DateTime.Today;
    public string Indicaciones { get; set; } = string.Empty;
}

public sealed class RecetaDetalleModel
{
    public Guid Id { get; set; }
    public Guid RecetaId { get; set; }
    public Guid MedicamentoId { get; set; }
    public string Dosis { get; set; } = string.Empty;
    public string Frecuencia { get; set; } = string.Empty;
    public int DuracionDias { get; set; }
}

public sealed class HospitalizacionModel
{
    public Guid Id { get; set; }
    public Guid PacienteId { get; set; }
    public Guid HabitacionId { get; set; }
    public DateTime FechaIngreso { get; set; } = DateTime.Now;
    public DateTime? FechaAlta { get; set; }
    public string Motivo { get; set; } = string.Empty;
}
