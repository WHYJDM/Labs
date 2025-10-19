using System.Xml.Serialization;

namespace Lab2Library;

/// <summary>
/// Represents the type of watches.
/// </summary>
public enum WatchesType                                             // какие типы часов у меня есть
{
    Electronic,
    Mechanic,
    Tower
}

/// <summary>
/// Represents a watch with its properties.
/// </summary>
public class Watches
{
    private int Id { get; set; }                                    // приватное свойство для хранения ID

    [XmlAttribute]                                                  // при сериализации в XML это будет атрибутом, а не тегом (атрибут хранит информацию) (тег это коробка)
    public string? Model { get; set; }                              // ? чтобы мог хранить  null

    public string? SerialNumber { get; set; }
    public WatchesType Type { get; set; }

    /// <summary>
    /// Creates a new instance of Watches.
    /// </summary>
    public static Watches Create(int id, string model, string serialNumber, WatchesType type)                  // метод для создания нового объекта часов
    {
        return new Watches { Id = id, Model = model, SerialNumber = serialNumber, Type = type };               // создает обьект и сразу ссылку дает
    }

    /// <summary>
    /// Prints the watch details to the console.
    /// </summary>
    public void PrintObject()
    {
        Console.WriteLine($"Id: {Id}, Model: {Model}, SerialNumber: {SerialNumber}, Type: {Type}");
    }
}