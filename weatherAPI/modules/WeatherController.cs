
[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly WeatherService _weatherService;

    public WeatherController(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("{city}")]
    public async Task<ActionResult<WeatherForecast>> Get(string city)
    {
        var weather = await _weatherService.GetWeatherAsync(city);
        return Ok(weather);
    }
}