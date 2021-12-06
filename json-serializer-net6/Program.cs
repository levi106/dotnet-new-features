using System.Text.Json;
using System.Text.Json.Serialization;

Person? person = new("Jane", "Doe");
byte[] utf8Json = JsonSerializer.SerializeToUtf8Bytes(person, MyJsonContext.Default.Person);
person = JsonSerializer.Deserialize(utf8Json, MyJsonContext.Default.Person);

Console.WriteLine($"{person}");

record Person(string FirstName, string LastName);

[JsonSerializable(typeof(Person))]
internal partial class MyJsonContext : JsonSerializerContext
{ }
