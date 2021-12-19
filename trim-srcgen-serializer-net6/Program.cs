using System.Text.Json;
using System.Text.Json.Serialization;

Message obj = new() { Data = "Hello World" };

string json = JsonSerializer.Serialize(obj, MyJsonContext.Default.Message);
obj = JsonSerializer.Deserialize<Message>(json, MyJsonContext.Default.Message);
Console.WriteLine($"{obj.Data}");

public struct Message
{
    public string Data { get; set; }
}

[JsonSerializable(typeof(Message))]
internal partial class MyJsonContext : JsonSerializerContext { }
