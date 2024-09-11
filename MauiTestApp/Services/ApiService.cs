using MauiTestApp.Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Text.Json;

namespace MauiTestApp.Services
{
    public static class ApiService
    {
        private const string WeatherApiKey = "a73034daae986c8ab83dc00a75ac550b";
        private const string WeatherApiUrl = "https://ru.api.openweathermap.org/data/2.5/forecast";

        public static async Task<Root> GetWeather(double latitude, double longitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format($"{WeatherApiUrl}?lat={latitude}&lon={longitude}&units=metric&appid={WeatherApiKey}"));

            return JsonConvert.DeserializeObject<Root>(response);
        }

        public static async Task<Root> GetWeatherByCity(string cityName)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format($"{WeatherApiUrl}?q={cityName}&units=metric&appid={WeatherApiKey}"));

            return JsonConvert.DeserializeObject<Root>(response);
        }
    }
}
