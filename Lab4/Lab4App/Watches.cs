using System.Text.Json;

namespace Lab4App;

public class Watches
{
    public required int Id { get; set; }

    public required string Model { get; set; }

    public required string SerialNumber { get; set; }
    public required WatchesType Type { get; set; }

    public static Watches Create(int id, string model, string serialNumber, WatchesType type)
    {
        return new Watches { Id = id, Model = model, SerialNumber = serialNumber, Type = type };
    }

    public void PrintObject()
    {
        Console.WriteLine($"Id: {Id}, Model: {Model}, SerialNumber: {SerialNumber}, Type: {Type}");
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public static Watches FromJson(string json)
    {
        return JsonSerializer.Deserialize<Watches>(json)!;
    }
}