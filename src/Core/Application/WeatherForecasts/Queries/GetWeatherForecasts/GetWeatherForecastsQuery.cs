using MauiCleanTodos.Shared.WeatehrForecasts;
using MediatR;

namespace MauiCleanTodos.Application.WeatherForecasts.Queries.GetWeatherForecasts;

public record GetWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecastDto>>;

public class GetWeatherForecastsQueryHandler : IRequestHandler<GetWeatherForecastsQuery, IEnumerable<WeatherForecastDto>>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<IEnumerable<WeatherForecastDto>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
    {
        var rng = new Random();

        return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecastDto
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        }));
    }
}
