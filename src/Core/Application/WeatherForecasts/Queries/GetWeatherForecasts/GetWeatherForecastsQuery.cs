using MauiCleanTodos.Shared.WeatehrForecasts;
using MediatR;

namespace MauiCleanTodos.Application.WeatherForecasts.Queries.GetWeatherForecasts;

public record GetWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecastDto>>;

public class GetWeatherForecastsQueryHandler : RequestHandler<GetWeatherForecastsQuery, IEnumerable<WeatherForecastDto>>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    protected override IEnumerable<WeatherForecastDto> Handle(GetWeatherForecastsQuery request)
    {
        var rng = new Random();

        return Enumerable.Range(1, 5).Select(index => new WeatherForecastDto
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        });
    }
}
