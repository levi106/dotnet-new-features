using System.Text.Json;
using System.Text.Json.Serialization;

WeatherForecast? weatherForecast = new()
    { Date = DateTime.Parse("2019-08-01"), TemperatureCelsius = 25, Summary = "Hot" };

string jsonString = JsonSerializer.Serialize(weatherForecast,
    SerializeOnlyContext.Default.WeatherForecast);

Console.WriteLine(jsonString);

// Source generation mode is currently only available for serialization.
//weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(
//    jsonString, SerializeOnlyContext.Default.WeatherForecast);

//Console.WriteLine($"Date={weatherForecast?.Date}");

weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(
    jsonString, MetadataOnlyContext.Default.WeatherForecast);

Console.WriteLine($"Date={weatherForecast?.Date}");

jsonString = JsonSerializer.Serialize(
    weatherForecast, MetadataOnlyContext.Default?.WeatherForecast!);

Console.WriteLine(jsonString);

public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureCelsius { get; set; }
    public string? Summary { get; set; }
}

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Serialization)]
[JsonSerializable(typeof(WeatherForecast))]
internal partial class SerializeOnlyContext : JsonSerializerContext
{ }

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(WeatherForecast))]
internal partial class MetadataOnlyContext : JsonSerializerContext
{ }

