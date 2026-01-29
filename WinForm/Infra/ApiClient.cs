using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

public sealed class ApiClient
{
    private readonly HttpClient _http;

    public ApiClient(string baseUrl)
    {
        var handler = new HttpClientHandler();

        _http = new HttpClient(handler)
        {
            BaseAddress = new Uri(baseUrl)
        };
    }

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<TResponse> PostAsync<TResponse>(string url, object body, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(body, JsonOpts);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var resp = await _http.PostAsync(url, content, ct);

        var respText = await resp.Content.ReadAsStringAsync(ct);

        if (!resp.IsSuccessStatusCode)
        {
            throw new Exception($"HTTP {(int)resp.StatusCode} {resp.ReasonPhrase}\n{respText}");
        }

        if (string.IsNullOrWhiteSpace(respText))
            return default!;

        return JsonSerializer.Deserialize<TResponse>(respText, JsonOpts)!;
    }

    public async Task<T> GetAsync<T>(string url, CancellationToken ct = default)
    {
        using var resp = await _http.GetAsync(url, ct);
        var text = await resp.Content.ReadAsStringAsync(ct);

        if (!resp.IsSuccessStatusCode)
            throw new Exception($"HTTP {(int)resp.StatusCode} {resp.ReasonPhrase}\n{text}");

        return JsonSerializer.Deserialize<T>(text, JsonOpts)!;
    }
}