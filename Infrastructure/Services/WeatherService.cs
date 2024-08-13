using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient client;
        private readonly IConfiguration configuration;

        public WeatherService(IHttpClientFactory _httpClientFactory, IConfiguration _configuration)
        {
            client = _httpClientFactory.CreateClient("WeatherClient");
            configuration = _configuration;
        }
        public async Task<Report> GetWeatherAsync(double latitude, double longitude)
        {
            var apiKey = configuration["WeatherApi:ApiKey"];
            var unit = configuration["WeatherApi:Units"];

            var requestUri = $"weather?lat={latitude}&lon={longitude}&appid={apiKey}&units={unit}";
            var response = await client.GetAsync(requestUri);

            var content = await response.Content.ReadAsStringAsync();
            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(content);

            return MapToReportModel(weatherResponse);
        }

        private Report MapToReportModel(WeatherResponse weatherResponse)
        {
            return new Report
            {
                WeatherId = weatherResponse.Weather[0].Id,
                Main = weatherResponse.Weather[0].Main,
                Description = weatherResponse.Weather[0].Description,
                Icon = weatherResponse.Weather[0].Icon,
                Temp = weatherResponse.Main.Temp,
                FeelsLike = weatherResponse.Main.FeelsLike,
                TempMin = weatherResponse.Main.TempMin,
                TempMax = weatherResponse.Main.TempMax,
                Pressure = weatherResponse.Main.Pressure,
                Humidity = weatherResponse.Main.Humidity,
                SeaLevel = weatherResponse.Main.SeaLevel ?? 0,
                GroundLevel = weatherResponse.Main.GrndLevel ?? 0,
                WindSpeed = weatherResponse.Wind.Speed,
                WindDegree = weatherResponse.Wind.Degree,
                WindGust = weatherResponse.Wind.Gust ?? 0,
                Clouds = weatherResponse.Clouds.All
            };
        }
    }
    #region Response
    public class WeatherResponse
    {
        public Weather[] Weather { get; set; }
        public Main Main { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class Main
    {
        public float Temp { get; set; }
        public float FeelsLike { get; set; }
        public float TempMin { get; set; }
        public float TempMax { get; set; }
        public float Pressure { get; set; }
        public float Humidity { get; set; }
        public float? SeaLevel { get; set; }
        public float? GrndLevel { get; set; }
    }

    public class Wind
    {
        public float Speed { get; set; }
        public float Degree { get; set; }
        public float? Gust { get; set; }
    }

    public class Clouds
    {
        public float All { get; set; }
    }
    #endregion
}