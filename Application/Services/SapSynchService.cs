using Application.Interfaces;
using Application.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Application.Services;

public class SapSynchService : ISapSynchService
{
    private readonly HttpClient _httpClient;
    private readonly SapSynchSettings _settings;

    public SapSynchService(HttpClient httpClient, IOptions<SapSynchSettings> options)
    {
        _httpClient = httpClient;
        _settings = options.Value;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _settings.TokenUrl);

        var body = new
        {
            grant_type = _settings.GrantType,
            client_id = _settings.ClientId,
            client_secret = _settings.ClientSecret
        };

        request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);
        var accessToken = doc.RootElement.GetProperty("access_token").GetString();

        return accessToken!;
    }

    public async Task<string> GetListDOFromSapAsync(object listDoParameters, string accessToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _settings.ListDOUrl);

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        request.Content = new StringContent(JsonSerializer.Serialize(listDoParameters), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetDetailDOFromSapAsync(object detailDoParameters, string accessToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _settings.DetailDOUrl);

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.Add("Cookie", "sap-usercontext=sap-client=300");

        request.Content = new StringContent(JsonSerializer.Serialize(detailDoParameters), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetCustomerFromSapAsync(object customerParams, string accessToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _settings.CustomerUrl); // <-- tambahkan ke config

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        request.Headers.Add("Cookie", "sap-usercontext=sap-client=300");

        request.Content = new StringContent(JsonSerializer.Serialize(customerParams), Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }


}
