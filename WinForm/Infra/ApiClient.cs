using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

public sealed class ApiClient
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions _json = new(JsonSerializerDefaults.Web);

    public ApiClient(string baseUrl)
    {
        _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }

    public async Task<T> GetAsync<T>(string url, CancellationToken ct = default)
    {
        var res = await _http.GetAsync(url, ct);
        var body = await res.Content.ReadAsStringAsync(ct);
        if (!res.IsSuccessStatusCode) throw new Exception($"{(int)res.StatusCode}: {body}");
        return JsonSerializer.Deserialize<T>(body, _json)!;
    }

    public async Task<TResponse> PostAsync<TResponse>(string url, object request, CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(request, _json);
        var res = await _http.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"), ct);
        var body = await res.Content.ReadAsStringAsync(ct);
        if (!res.IsSuccessStatusCode) throw new Exception($"{(int)res.StatusCode}: {body}");
        return JsonSerializer.Deserialize<TResponse>(body, _json)!;
    }
}
