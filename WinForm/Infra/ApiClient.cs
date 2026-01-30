using System.Net.Http;
using System.Text;
using System.Text.Json;
using Serilog;

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

        // İstersen default timeout ayarla
        // _http.Timeout = TimeSpan.FromSeconds(30);
    }

    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<TResponse> PostAsync<TResponse>(string url, object body, CancellationToken ct = default)
    {
        try
        {
            var json = JsonSerializer.Serialize(body, JsonOpts);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var resp = await _http.PostAsync(url, content, ct);
            var respText = await resp.Content.ReadAsStringAsync(ct);

            if (!resp.IsSuccessStatusCode)
            {
                // HTTP hata cevabını logla
                Log.Error("API POST FAILED | Url={Url} | Status={StatusCode} {ReasonPhrase} | Body={Body} | Response={Response}",
                    url,
                    (int)resp.StatusCode,
                    resp.ReasonPhrase,
                    json,
                    respText);

                throw new Exception($"HTTP {(int)resp.StatusCode} {resp.ReasonPhrase}\n{respText}");
            }

            if (string.IsNullOrWhiteSpace(respText))
                return default!;

            return JsonSerializer.Deserialize<TResponse>(respText, JsonOpts)!;
        }
        catch (TaskCanceledException ex) when (!ct.IsCancellationRequested)
        {
            // Timeout senaryosu
            Log.Error(ex, "API POST TIMEOUT | Url={Url}", url);
            throw;
        }
        catch (HttpRequestException ex)
        {
            // Connection refused / SSL / DNS vs
            Log.Error(ex, "API POST CONNECTION ERROR | Url={Url}", url);
            throw;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "API POST UNEXPECTED ERROR | Url={Url}", url);
            throw;
        }
    }

    public async Task<T> GetAsync<T>(string url, CancellationToken ct = default)
    {
        try
        {
            using var resp = await _http.GetAsync(url, ct);
            var text = await resp.Content.ReadAsStringAsync(ct);

            if (!resp.IsSuccessStatusCode)
            {
                Log.Error("API GET FAILED | Url={Url} | Status={StatusCode} {ReasonPhrase} | Response={Response}",
                    url,
                    (int)resp.StatusCode,
                    resp.ReasonPhrase,
                    text);

                throw new Exception($"HTTP {(int)resp.StatusCode} {resp.ReasonPhrase}\n{text}");
            }

            return JsonSerializer.Deserialize<T>(text, JsonOpts)!;
        }
        catch (TaskCanceledException ex) when (!ct.IsCancellationRequested)
        {
            Log.Error(ex, "API GET TIMEOUT | Url={Url}", url);
            throw;
        }
        catch (HttpRequestException ex)
        {
            Log.Error(ex, "API GET CONNECTION ERROR | Url={Url}", url);
            throw;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "API GET UNEXPECTED ERROR | Url={Url}", url);
            throw;
        }
    }
}