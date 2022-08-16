using Microsoft.Extensions.Options;

namespace MauiCleanTodos.ApiClient.Services;

public interface IWeatherService
{
    Task<List<WeatherForecastDto>> GetForecastsAsync();
}

public class WeatherService : BaseService, IWeatherService
{
    private readonly WeatherForecastClient _client;

    public WeatherService(IHttpClientFactory httpClientFactory, IOptions<ApiClientOptions> options) : base(httpClientFactory, options)
    {
        _client = new WeatherForecastClient(_baseUrl, _httpClient);
    }

    public async Task<List<WeatherForecastDto>> GetForecastsAsync()
    {
        var forecasts = await _client.GetAsync();

        return forecasts.ToList();
    }
}
