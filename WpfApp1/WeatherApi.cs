using System.Net.Http;
using System.Text.Json;

namespace WpfApp1;

public static class WeatherApi
{
    public static async Task<WeatherDataSerializer> GetWeatherDetails (string location) {
        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Location not set");
        
        var url = $"https://api.weatherapi.com/v1/current.json?key=db426dbad0b94940a22150528250308&q={location}&aqi=no";

        var client = new HttpClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<WeatherDataSerializer>(jsonResponse);
        if(data == null) throw new Exception("Failed to deserialize weather data.");
        
        return data;
    }
    
}