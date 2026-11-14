using System.Net.Http.Json;
using System.Text.Json;

namespace SistemaPacientes.Blazor.Services;

/// <summary>
/// Cliente HTTP genérico que consume los endpoints CRUD de la API.
/// </summary>
public sealed class ApiService
{
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web);

    private readonly HttpClient _http;

    public ApiService(HttpClient http) => _http = http;

    public async Task<List<T>> GetAllAsync<T>(string path)
    {
        var result = await _http.GetFromJsonAsync<List<T>>(path, Options);
        return result ?? new List<T>();
    }

    public async Task<T?> GetByIdAsync<T>(string path, Guid id)
        => await _http.GetFromJsonAsync<T>($"{path}/{id}", Options);

    public async Task<T> CreateAsync<T>(string path, T item)
    {
        var response = await _http.PostAsJsonAsync(path, item, Options);
        await EnsureSuccessAsync(response);
        return (await response.Content.ReadFromJsonAsync<T>(Options))!;
    }

    public async Task<T> UpdateAsync<T>(string path, Guid id, T item)
    {
        var response = await _http.PutAsJsonAsync($"{path}/{id}", item, Options);
        await EnsureSuccessAsync(response);
        return (await response.Content.ReadFromJsonAsync<T>(Options))!;
    }

    public async Task DeleteAsync(string path, Guid id)
    {
        var response = await _http.DeleteAsync($"{path}/{id}");
        await EnsureSuccessAsync(response);
    }

    private static async Task EnsureSuccessAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var message = $"Error {(int)response.StatusCode} al comunicarse con la API.";

        try
        {
            using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            if (document.RootElement.TryGetProperty("error", out var error))
            {
                message = error.GetString() ?? message;
            }
        }
        catch (JsonException)
        {
            // La respuesta no contenía un JSON con el detalle del error.
        }

        throw new InvalidOperationException(message);
    }
}
