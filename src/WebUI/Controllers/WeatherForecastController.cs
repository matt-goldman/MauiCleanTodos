using MauiCleanTodos.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using MauiCleanTodos.Shared.WeatehrForecasts;
using Microsoft.AspNetCore.Mvc;

namespace MauiCleanTodos.WebUI.Controllers;

public class WeatherForecastController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<WeatherForecastDto>> Get()
    {
        return await Mediator.Send(new GetWeatherForecastsQuery());
    }
}
