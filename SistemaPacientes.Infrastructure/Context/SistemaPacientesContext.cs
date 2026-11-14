using Microsoft.EntityFrameworkCore;
using SistemaPacientes.Domain.Core;
using SistemaPacientes.Domain.Entities;

namespace SistemaPacientes.Infrastructure.Context;

/// <summary>
/// Contexto de Entity Framework Core para el Sistema de Pacientes.
/// Expone las 13 tablas que conforman el modelo de datos.
/// </summary>
public sealed class SistemaPacientesContext : DbContext
{
    public SistemaPacientesContext(DbContextOptions<SistemaPacientesContext> options) : base(options) { }

    public DbSet<Especialidad> Especialidades => Set<Especialidad>();
    public DbSet<SeguroMedico> SegurosMedicos => Set<SeguroMedico>();
    public DbSet<Habitacion> Habitaciones => Set<Habitacion>();
    public DbSet<Medicamento> Medicamentos => Set<Medicamento>();
    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<Medico> Medicos => Set<Medico>();
    public DbSet<Cita> Citas => Set<Cita>();
    public DbSet<Consulta> Consultas => Set<Consulta>();
    public DbSet<Diagnostico> Diagnosticos => Set<Diagnostico>();
    public DbSet<Tratamiento> Tratamientos => Set<Tratamiento>();
    public DbSet<Receta> Recetas => Set<Receta>();
    public DbSet<RecetaDetalle> RecetaDetalles => Set<RecetaDetalle>();
    public DbSet<Hospitalizacion> Hospitalizaciones => Set<Hospitalizacion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Tabla padre "Persona": Paciente y Medico comparten la misma forma de persona,
        // se configuran como tablas independientes para simplificar el acceso relacional.
        ConfigurarEspecialidad(modelBuilder);
        ConfigurarSeguroMedico(modelBuilder);
        ConfigurarHabitacion(modelBuilder);
        ConfigurarMedicamento(modelBuilder);
        ConfigurarPaciente(modelBuilder);
        ConfigurarMedico(modelBuilder);
        ConfigurarCita(modelBuilder);
        ConfigurarConsulta(modelBuilder);
        ConfigurarDiagnostico(modelBuilder);
        ConfigurarTratamiento(modelBuilder);
        ConfigurarReceta(modelBuilder);
        ConfigurarRecetaDetalle(modelBuilder);
        ConfigurarHospitalizacion(modelBuilder);

        Seed(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.MarkUpdated();
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    private static void ConfigurarEspecialidad(ModelBuilder mb)
    {
        mb.Entity<Especialidad>(e =>
        {
            e.ToTable("Especialidades");
            e.HasKey(x => x.Id);
            e.Property(x => x.Codigo).HasMaxLength(20).IsRequired();
            e.Property(x => x.Nombre).HasMaxLength(100).IsRequired();
            e.Property(x => x.Descripcion).HasMaxLength(250);
            e.HasIndex(x => x.Codigo).IsUnique();
        });
    }

    private static void ConfigurarSeguroMedico(ModelBuilder mb)
    {
        mb.Entity<SeguroMedico>(e =>
        {
            e.ToTable("SegurosMedicos");
            e.HasKey(x => x.Id);
            e.Property(x => x.Codigo).HasMaxLength(20).IsRequired();
            e.Property(x => x.Nombre).HasMaxLength(100).IsRequired();
            e.Property(x => x.Cobertura).HasPrecision(18, 2);
            e.Property(x => x.Telefono).HasMaxLength(20);
            e.HasIndex(x => x.Codigo).IsUnique();
        });
    }

    private static void ConfigurarHabitacion(ModelBuilder mb)
    {
        mb.Entity<Habitacion>(e =>
        {
            e.ToTable("Habitaciones");
            e.HasKey(x => x.Id);
            e.Property(x => x.Numero).HasMaxLength(10).IsRequired();
            e.Property(x => x.Tipo).HasMaxLength(50).IsRequired();
            e.HasIndex(x => x.Numero).IsUnique();
        });
    }

    private static void ConfigurarMedicamento(ModelBuilder mb)
    {
        mb.Entity<Medicamento>(e =>
        {
            e.ToTable("Medicamentos");
            e.HasKey(x => x.Id);
            e.Property(x => x.Codigo).HasMaxLength(20).IsRequired();
            e.Property(x => x.Nombre).HasMaxLength(120).IsRequired();
            e.Property(x => x.Presentacion).HasMaxLength(60);
            e.Property(x => x.Concentracion).HasMaxLength(60);
            e.HasIndex(x => x.Codigo).IsUnique();
        });
    }

    private static void ConfigurarPaciente(ModelBuilder mb)
    {
        mb.Entity<Paciente>(e =>
        {
            e.ToTable("Pacientes");
            e.HasKey(x => x.Id);
            e.Property(x => x.NumeroExpediente).HasMaxLength(20).IsRequired();
            e.Property(x => x.Nombre).HasMaxLength(80).IsRequired();
            e.Property(x => x.Apellido).HasMaxLength(80).IsRequired();
            e.Property(x => x.Documento).HasMaxLength(30).IsRequired();
            e.Property(x => x.Sexo).HasMaxLength(20);
            e.Property(x => x.Telefono).HasMaxLength(20);
            e.Property(x => x.Correo).HasMaxLength(120);
            e.Property(x => x.Direccion).HasMaxLength(200);
            e.Property(x => x.TipoSangre).HasMaxLength(5);
            e.Ignore(x => x.NombreCompleto);
            e.HasIndex(x => x.NumeroExpediente).IsUnique();
            e.HasIndex(x => x.Documento).IsUnique();

            e.HasOne(x => x.SeguroMedico)
                .WithMany(s => s.Pacientes)
                .HasForeignKey(x => x.SeguroMedicoId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }

    private static void ConfigurarMedico(ModelBuilder mb)
    {
        mb.Entity<Medico>(e =>
        {
            e.ToTable("Medicos");
            e.HasKey(x => x.Id);
            e.Property(x => x.NumeroColegiado).HasMaxLength(20).IsRequired();
            e.Property(x => x.Nombre).HasMaxLength(80).IsRequired();
            e.Property(x => x.Apellido).HasMaxLength(80).IsRequired();
            e.Property(x => x.Correo).HasMaxLength(120);
            e.Property(x => x.Telefono).HasMaxLength(20);
            e.Ignore(x => x.NombreCompleto);
            e.HasIndex(x => x.NumeroColegiado).IsUnique();

            e.HasOne(x => x.Especialidad)
                .WithMany(s => s.Medicos)
                .HasForeignKey(x => x.EspecialidadId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigurarCita(ModelBuilder mb)
    {
        mb.Entity<Cita>(e =>
        {
            e.ToTable("Citas");
            e.HasKey(x => x.Id);
            e.Property(x => x.Motivo).HasMaxLength(250).IsRequired();
            e.Property(x => x.Estado).HasMaxLength(30).IsRequired();

            e.HasOne(x => x.Paciente)
                .WithMany(p => p.Citas)
                .HasForeignKey(x => x.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Medico)
                .WithMany(m => m.Citas)
                .HasForeignKey(x => x.MedicoId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigurarConsulta(ModelBuilder mb)
    {
        mb.Entity<Consulta>(e =>
        {
            e.ToTable("Consultas");
            e.HasKey(x => x.Id);
            e.Property(x => x.Sintomas).HasMaxLength(500);
            e.Property(x => x.Observaciones).HasMaxLength(500);

            e.HasOne(x => x.Cita)
                .WithMany()
                .HasForeignKey(x => x.CitaId)
                .OnDelete(DeleteBehavior.SetNull);

            e.HasOne(x => x.Paciente)
                .WithMany(p => p.Consultas)
                .HasForeignKey(x => x.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Medico)
                .WithMany(m => m.Consultas)
                .HasForeignKey(x => x.MedicoId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigurarDiagnostico(ModelBuilder mb)
    {
        mb.Entity<Diagnostico>(e =>
        {
            e.ToTable("Diagnosticos");
            e.HasKey(x => x.Id);
            e.Property(x => x.Codigo).HasMaxLength(20).IsRequired();
            e.Property(x => x.Descripcion).HasMaxLength(300).IsRequired();
            e.Property(x => x.Gravedad).HasMaxLength(30);

            e.HasOne(x => x.Consulta)
                .WithMany(c => c.Diagnosticos)
                .HasForeignKey(x => x.ConsultaId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigurarTratamiento(ModelBuilder mb)
    {
        mb.Entity<Tratamiento>(e =>
        {
            e.ToTable("Tratamientos");
            e.HasKey(x => x.Id);
            e.Property(x => x.Descripcion).HasMaxLength(300).IsRequired();
            e.Property(x => x.Indicaciones).HasMaxLength(500);

            e.HasOne(x => x.Consulta)
                .WithMany(c => c.Tratamientos)
                .HasForeignKey(x => x.ConsultaId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigurarReceta(ModelBuilder mb)
    {
        mb.Entity<Receta>(e =>
        {
            e.ToTable("Recetas");
            e.HasKey(x => x.Id);
            e.Property(x => x.Indicaciones).HasMaxLength(500);

            e.HasOne(x => x.Consulta)
                .WithMany(c => c.Recetas)
                .HasForeignKey(x => x.ConsultaId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Medico)
                .WithMany(m => m.Recetas)
                .HasForeignKey(x => x.MedicoId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Paciente)
                .WithMany(p => p.Recetas)
                .HasForeignKey(x => x.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigurarRecetaDetalle(ModelBuilder mb)
    {
        mb.Entity<RecetaDetalle>(e =>
        {
            e.ToTable("RecetaDetalles");
            e.HasKey(x => x.Id);
            e.Property(x => x.Dosis).HasMaxLength(60).IsRequired();
            e.Property(x => x.Frecuencia).HasMaxLength(60);

            e.HasOne(x => x.Receta)
                .WithMany(r => r.Detalles)
                .HasForeignKey(x => x.RecetaId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Medicamento)
                .WithMany(m => m.RecetaDetalles)
                .HasForeignKey(x => x.MedicamentoId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigurarHospitalizacion(ModelBuilder mb)
    {
        mb.Entity<Hospitalizacion>(e =>
        {
            e.ToTable("Hospitalizaciones");
            e.HasKey(x => x.Id);
            e.Property(x => x.Motivo).HasMaxLength(300).IsRequired();

            e.HasOne(x => x.Paciente)
                .WithMany(p => p.Hospitalizaciones)
                .HasForeignKey(x => x.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Habitacion)
                .WithMany(h => h.Hospitalizaciones)
                .HasForeignKey(x => x.HabitacionId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void Seed(ModelBuilder mb)
    {
        var especialidadGeneral = new Especialidad { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Codigo = "GEN", Nombre = "Medicina General", Descripcion = "Atención médica general" };
        var especialidadCardio = new Especialidad { Id = Guid.Parse("11111111-1111-1111-1111-111111111112"), Codigo = "CAR", Nombre = "Cardiología", Descripcion = "Especialidad del corazón" };
        var seguroBasico = new SeguroMedico { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Codigo = "SEG-BAS", Nombre = "Seguro Básico", Cobertura = 60m, Telefono = "800-1000" };
        var habitacion101 = new Habitacion { Id = Guid.Parse("33333333-3333-3333-3333-333333333331"), Numero = "101", Piso = 1, Tipo = "Individual", Disponible = true };
        var medicamentoParacetamol = new Medicamento { Id = Guid.Parse("44444444-4444-4444-4444-444444444441"), Codigo = "MED-001", Nombre = "Paracetamol", Presentacion = "Tableta", Concentracion = "500 mg" };
        var pacienteDemo = new Paciente
        {
            Id = Guid.Parse("55555555-5555-5555-5555-555555555551"),
            NumeroExpediente = "EXP-0001",
            Nombre = "Juan",
            Apellido = "Pérez",
            Documento = "12345678",
            FechaNacimiento = new DateTime(1990, 5, 20, 0, 0, 0, DateTimeKind.Utc),
            Sexo = "Masculino",
            Telefono = "555-0101",
            Correo = "juan.perez@example.com",
            Direccion = "Calle Principal 123",
            TipoSangre = "O+",
            SeguroMedicoId = seguroBasico.Id
        };
        var medicoDemo = new Medico
        {
            Id = Guid.Parse("66666666-6666-6666-6666-666666666661"),
            NumeroColegiado = "COL-0001",
            Nombre = "Ana",
            Apellido = "Gómez",
            Correo = "ana.gomez@example.com",
            Telefono = "555-0201",
            EspecialidadId = especialidadGeneral.Id
        };

        mb.Entity<Especialidad>().HasData(especialidadGeneral, especialidadCardio);
        mb.Entity<SeguroMedico>().HasData(seguroBasico);
        mb.Entity<Habitacion>().HasData(habitacion101);
        mb.Entity<Medicamento>().HasData(medicamentoParacetamol);
        mb.Entity<Paciente>().HasData(pacienteDemo);
        mb.Entity<Medico>().HasData(medicoDemo);
    }
}
