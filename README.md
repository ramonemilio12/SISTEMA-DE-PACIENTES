# Sistema de Pacientes — API .NET + Blazor WebAssembly

CRUD completo de un **Sistema de Pacientes** (gestión hospitalaria) construido con **arquitectura N-Capas**.
El API en **.NET 8 (ASP.NET Core Web API)** está construido **con interfaces** (contratos para
repositorios y servicios) y es consumido por un front-end **funcional de Blazor WebAssembly**.

## Contenido

- [Arquitectura N-Capas](#arquitectura-n-capas)
- [Conceptos de N-Capas aplicados](#conceptos-de-n-capas-aplicados)
- [Modelo de datos (13 tablas)](#modelo-de-datos-13-tablas)
- [Tecnologías](#tecnologías)
- [Estructura del proyecto](#estructura-del-proyecto)
- [Requisitos](#requisitos)
- [Cómo ejecutar](#cómo-ejecutar)
- [Endpoints del API](#endpoints-del-api)

## Arquitectura N-Capas

El proyecto está dividido en **4 capas** con dependencias en un solo sentido
(`Api → Application → Infrastructure → Domain`). Ninguna capa de nivel superior
depende de los detalles de implementación de la inferior: solo de sus **interfaces**.

| Capa | Proyecto | Responsabilidad |
|------|----------|-----------------|
| **Domain** (Dominio) | `SistemaPacientes.Domain` | Entidades de negocio y **interfaces de repositorio** (`IRepository<T>`, `IPacienteRepository`, ...). No depende de nada. |
| **Infrastructure** (Infraestructura) | `SistemaPacientes.Infrastructure` | Acceso a datos con **EF Core**: `DbContext`, repositorios concretos e **inyección de dependencias**. |
| **Application** (Aplicación) | `SistemaPacientes.Application` | **DTOs**, **interfaces de servicio**, lógica de negocio y validaciones, e inyección de dependencias. |
| **Api** (Presentación – Backend) | `SistemaPacientes.Api` | **Controladores REST**, middleware de excepciones, Swagger y `Program.cs`. |
| **Blazor** (Presentación – Frontend) | `SistemaPacientes.Blazor` | Cliente **Blazor WebAssembly** que consume el API por HTTP. |

Flujo de una petición:

```
Blazor (UI) → HttpClient → Api/Controller → I<Entidad>Service (Application)
           → I<Entidad>Repository (Domain) → Repository<T> (Infrastructure) → EF Core → SQL Server
```

## Conceptos de N-Capas aplicados

- **Separación en capas** con responsabilidades únicas y dependencias unidireccionales.
- **Programación orientada a interfaces**: `IRepository<T>` genérico + interfaces específicas
  (`IPacienteRepository`, `IEspecialidadRepository`, `IMedicoRepository`, `ICitaRepository`, ...)
  y una **interfaz de servicio por cada entidad** (`IPacienteService`, `IEspecialidadService`, ...).
- **Patrón Repositorio**: `Repository<T>` base (CRUD genérico) + repositorios sellados con consultas
  específicas (búsquedas, validaciones de unicidad, `Include` de relaciones).
- **Patrón Servicio**: la lógica de negocio y las validaciones viven en la capa **Application**, no en los controladores.
- **DTOs**: se exponen `Dto` (lectura) y `WriteDto` (escritura) para no filtrar las entidades del dominio.
- **Inyección de Dependencias**: cada capa registra sus servicios mediante métodos de extensión
  (`AddInfrastructure(connectionString)` y `AddApplication()`).
- **Inversión de dependencias**: las capas altas dependen de **abstracciones** (`Domain`), no de implementaciones.
- **Manejo centralizado de errores**: middleware que traduce `BusinessException` → HTTP 400 y errores internos → HTTP 500.
- **Herencia base**: entidad base `BaseEntity` (Id, CreatedAt, UpdatedAt) con `MarkUpdated()`.

## Modelo de datos (13 tablas)

| # | Tabla | Descripción |
|---|-------|-------------|
| 1 | `Especialidades` | Especialidades médicas (código, nombre, descripción). |
| 2 | `SegurosMedicos` | Aseguradoras / coberturas (código, nombre, cobertura %). |
| 3 | `Habitaciones` | Habitaciones del hospital (número, tipo, disponibilidad). |
| 4 | `Medicamentos` | Catálogo de medicamentos (código, nombre, presentación, precio). |
| 5 | `Pacientes` | Pacientes (expediente, datos personales, seguro). |
| 6 | `Medicos` | Médicos (colegiado, especialidad). |
| 7 | `Citas` | Citas médicas (paciente, médico, fecha/hora, estado). |
| 8 | `Consultas` | Consultas realizadas a partir de una cita. |
| 9 | `Diagnosticos` | Diagnósticos asociados a una consulta. |
| 10 | `Tratamientos` | Tratamientos derivados de un diagnóstico. |
| 11 | `Recetas` | Recetas médicas (consulta, fecha). |
| 12 | `RecetaDetalles` | Detalle de cada receta (medicamento, cantidad, indicaciones). |
| 13 | `Hospitalizaciones` | Ingresos hospitalarios (paciente, habitación, fechas, motivo). |

La base de datos se crea automáticamente con `Database.EnsureCreated()` y se **siembra** con
datos iniciales (`HasData`) la primera vez que arranca el API.

## Tecnologías

- **.NET 8** / C# 12
- **ASP.NET Core Web API** + **Swagger**
- **Entity Framework Core 8** (proveedor **SQL Server / LocalDB**)
- **Blazor WebAssembly** (Standalone) + `HttpClient` / `System.Net.Http.Json`
- Patrones: Repositorio, Servicio, DTO, Inyección de Dependencias

## Estructura del proyecto

```
SistemaPacientes/
├─ SistemaPacientes.sln
├─ SistemaPacientes.Domain/           # Entidades + interfaces de repositorio
│  ├─ Core/BaseEntity.cs
│  ├─ Entities/                       # 13 entidades
│  └─ Interfaces/                     # IRepository<T> + interfaces específicas
├─ SistemaPacientes.Infrastructure/   # EF Core: DbContext + repositorios + DI
│  ├─ Context/SistemaPacientesContext.cs
│  ├─ Repositories/
│  └─ DependencyInjection.cs
├─ SistemaPacientes.Application/      # DTOs + servicios + DI
│  ├─ Dtos/
│  ├─ Interfaces/
│  ├─ Services/
│  └─ DependencyInjection.cs
├─ SistemaPacientes.Api/              # Controladores REST + middleware + Program.cs
│  ├─ Controllers/
│  ├─ Middleware/ExceptionHandlingMiddleware.cs
│  └─ Program.cs
└─ SistemaPacientes.Blazor/           # Cliente Blazor WebAssembly
   ├─ Components/CrudManager.razor    # Componente CRUD genérico reutilizable
   ├─ Models/Models.cs                # 13 modelos
   ├─ Services/ApiService.cs          # Cliente HTTP genérico
   └─ Pages/                          # 13 páginas CRUD + Home
```

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server LocalDB** (viene con Visual Studio; también vale SQL Server Express).
  La cadena de conexión por defecto es:
  `Server=(localdb)\mssqllocaldb;Database=SistemaPacientesDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True`

## Cómo ejecutar

Se deben ejecutar **dos proyectos**: primero el API y luego el Blazor.

### 1) API (`SistemaPacientes.Api`) — http://localhost:5100

```bash
cd SistemaPacientes
dotnet run --project SistemaPacientes.Api --launch-profile http
```

Se abrirá **Swagger** en `http://localhost:5100/swagger`. En el primer arranque se crea
la base de datos `SistemaPacientesDb` en LocalDB y se siembran los datos.

### 2) Blazor WebAssembly (`SistemaPacientes.Blazor`) — http://localhost:5200

En otra terminal:

```bash
cd SistemaPacientes
dotnet run --project SistemaPacientes.Blazor --launch-profile http
```

Abre `http://localhost:5200` en el navegador. El front consume el API en `http://localhost:5100`
(configurado en [Program.cs](SistemaPacientes.Blazor/Program.cs)).

> Nota: si cambias el puerto del API, actualiza `apiBaseUrl` en `SistemaPacientes.Blazor/Program.cs`.

## Endpoints del API

Cada recurso expone el CRUD completo: `GET` (listado), `GET/{id}`, `POST`, `PUT/{id}`, `DELETE/{id}`.

| Recurso | Ruta base |
|---------|-----------|
| Especialidades | `/api/especialidades` |
| Seguros médicos | `/api/seguros-medicos` |
| Habitaciones | `/api/habitaciones` |
| Medicamentos | `/api/medicamentos` |
| Pacientes | `/api/pacientes` |
| Médicos | `/api/medicos` |
| Citas | `/api/citas` |
| Consultas | `/api/consultas` |
| Diagnósticos | `/api/diagnosticos` |
| Tratamientos | `/api/tratamientos` |
| Recetas | `/api/recetas` |
| Receta detalles | `/api/receta-detalles` |
| Hospitalizaciones | `/api/hospitalizaciones` |
