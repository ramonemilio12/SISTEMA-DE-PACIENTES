namespace SistemaPacientes.Blazor.Components;

/// <summary>
/// Definición de una columna reutilizable para el componente genérico de CRUD.
/// </summary>
public sealed record ColumnDef<TItem>(string Header, Func<TItem, object?> Value);
