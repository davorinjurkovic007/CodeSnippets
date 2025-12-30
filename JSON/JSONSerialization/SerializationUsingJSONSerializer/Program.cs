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

// 1. Basic serialization
// Serialize
string jsonString = JsonSerializer.Serialize(weatherForecast);

// Print the serialized JSON
Console.WriteLine(jsonString);
Console.ReadLine();
Console.Clear();

// 2. Serialization but with generics
// Specify the class of the object to be serialized when calling the Serialize method
string jsonStringGenerics = JsonSerializer.Serialize<WeatherForecast>(weatherForecast);

// Write to the console the serialized JSON text
Console.WriteLine(jsonStringGenerics);
Console.ReadLine();
Console.Clear();

// 3. Serialization to a file
// Name of the file where the JSON string is stored
string fileName = "WeatherForecast.json";

// Write to the console the serialized JSON text
File.WriteAllText(fileName, jsonString);
