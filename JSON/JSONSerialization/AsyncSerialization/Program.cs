// See https://aka.ms/new-console-template for more information
using SerializationUsingJSONSerializer;
using System.Text.Json;

Console.WriteLine("Hello, World!");

// Create a new WeatherForecast object
var weatherForecast = new WeatherForecast
{
    Date = DateTime.Parse("2021-12-01"),
    TemperatureCelisius = 25,
    Summary = "Hot"
};

// Name of the file where the JSON string is stored
string fileName = "WeatherForecast.json";

// Stream write data to a local file
using FileStream createStream = File.Create(fileName);

// Serialize it
await JsonSerializer.SerializeAsync(createStream, weatherForecast);

// Await and dispose the stream
await createStream.DisposeAsync();
Console.Clear();
