using System.Text.Json;

Message obj = new() { Data = "Hello World" };

string json = JsonSerializer.Serialize(obj);
obj = JsonSerializer.Deserialize<Message>(json);
Console.WriteLine($"{obj.Data}");

public struct Message
{
    public string Data { get; set; }
}