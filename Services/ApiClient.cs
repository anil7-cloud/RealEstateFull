using System.Net.Http.Json;
using REAL_ESTATE_CLEAN.Core.Persistence;

namespace REAL_ESTATE_CLEAN.Services;

public class ApiClient
{
    private readonly HttpClient _http;

    public ApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Property>> GetProperties()
        => await _http.GetFromJsonAsync<List<Property>>("http://localhost:5019/api/properties")
           ?? new();

    public async Task AddProperty(Property p)
        => await _http.PostAsJsonAsync("http://localhost:5019/api/property", p);
}
