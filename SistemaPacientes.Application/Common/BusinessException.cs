namespace SistemaPacientes.Application.Common;

/// <summary>
/// Excepción de regla de negocio. La API la traduce a un HTTP 400.
/// </summary>
public sealed class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}
