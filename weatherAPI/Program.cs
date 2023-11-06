using System;
using System.Net.Http;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}
else
{
   app.UseDefaultFiles();
   app.UseStaticFiles();
}

public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }
}

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherForecast> GetWeatherAsync(string city)
    {
        var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=<APIKEY>&units=metric");
        response.EnsureSuccessStatusCode();

        var weatherData = await response.Content.ReadAsAsync<WeatherData>();
        return new WeatherForecast
        {
            Date = DateTime.Now,
            TemperatureC = (int)weatherData.Main.Temp,
            Summary = weatherData.Weather[0].Description
        };
    }
}

public class WeatherData
{
    public MainData Main { get; set; }
    public WeatherData[] Weather { get; set; }
}

public class MainData
{
    public float Temp { get; set; }
}


app.Run();
