// тут сер/десер в XML поиск и изменение атрибутов через XD и XmlD


using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Lab2Library;

/// <summary>
/// Handles XML serialization, deserialization, and manipulation operations.
/// </summary>
public class XmlHandler
{
    private readonly XmlSerializer _serializer; // поля классов _с этой штучкой в начале 

    public XmlHandler()
    {
        _serializer = new XmlSerializer(typeof(List<Watches>));
    }

    /// <summary>
    /// Serializes a list of watches to an XML file.
    /// </summary>
    public void SerializeWatches(List<Watches> watches, string fileName)
    {
        try
        {
            using var writer = new StreamWriter(fileName); // StreamWriter умеет записывать текст в файл
            _serializer.Serialize(writer, watches);                     // список превращается в xml
            Console.WriteLine("Serialized to " + fileName);
        }
        catch
        {
            Console.WriteLine("Error");
        }
    }

    /// <summary>
    /// Prints the content of an XML file.
    /// </summary>
    public void PrintFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("File does not exist.");
            return;
        }
        Console.WriteLine(File.ReadAllText(fileName));            // читает весь текст возвращет как строку (вывод на экран то что в XML)
    }

    /// <summary>
    /// Deserializes watches from an XML file and prints them.
    /// </summary>
    public void DeserializeAndPrint(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("File does not exist.");
            return;
        }
        try
        {
            using var reader = new StreamReader(fileName);   // умеет читать текст из файла
            var deserialized = (List<Watches>)_serializer.Deserialize(reader);
            PrintWatches(deserialized);
        }
        catch
        {
            Console.WriteLine("Error");
        }
    }

    /// <summary>
    /// Finds and prints model values using XDocument.
    /// </summary>
    public void FindModelsXDocument(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("File does not exist.");
            return;
        }
        try
        {
            var doc = XDocument.Load(fileName);
            var models = doc.Descendants("Watches")  // ищет все теги watches   
                .Attributes("Model")     // берет атрибут model из каждого тега 
                .Select(a => a.Value);   // делает Model1 Model2 в итоге models это список всех моделей из XML
            Console.WriteLine("Models: " + string.Join(", ", models) + ",");
        }
        catch
        {
            Console.WriteLine("Error");
        }
    }

    /// <summary>
    /// Finds and prints model values using XmlDocument.
    /// </summary>
    public void FindModelsXmlDocument(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("File does not exist.");
            return;
        }
        try
        {
            var doc = new XmlDocument();
            doc.Load(fileName);
            var nodes = doc.SelectNodes("//Watches/@Model");                // nodes список всех атрибутов Model в документе
            Console.Write("Models: ");
            foreach (XmlAttribute attr in nodes)
            {
                Console.Write(attr.Value + ", ");                           // attr.Value значение атрибута (Model1)
            }
            Console.WriteLine();
        }
        catch
        {
            Console.WriteLine("Error");
        }
    }

    /// <summary>
    /// Modifies an attribute using XDocument.
    /// </summary>
    public void ModifyAttributeXDocument(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("File does not exist.");
            return;
        }
        Console.Write("Enter attribute name: ");
        var attrName = Console.ReadLine();
        Console.Write("Enter element number (1-based): ");
        if (!int.TryParse(Console.ReadLine(), out var index) || index < 1)
        {
            Console.WriteLine("Invalid index.");
            return;
        }
        Console.Write("Enter new value: ");
        var newValue = Console.ReadLine();

        try
        {
            var doc = XDocument.Load(fileName);
            var element = doc.Descendants("Watches")
                .Skip(index - 1)
                .FirstOrDefault();
            if (element == null)
            {
                Console.WriteLine("Element not found.");
                return;
            }
            var attr = element.Attribute(attrName);
            if (attr == null)
            {
                Console.WriteLine("Attribute not found.");
                return;
            }
            attr.Value = newValue;
            doc.Save(fileName);
            Console.WriteLine("Modified.");
        }
        catch
        {
            Console.WriteLine("Error");
        }
    }

    /// <summary>
    /// Modifies an attribute using XmlDocument.
    /// </summary>
    public void ModifyAttributeXmlDocument(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("File does not exist.");
            return;
        }
        Console.Write("Enter attribute name: ");
        var attrName = Console.ReadLine();
        Console.Write("Enter element number (1-based): ");
        if (!int.TryParse(Console.ReadLine(), out var index) || index < 1)
        {
            Console.WriteLine("Invalid index.");
            return;
        }
        Console.Write("Enter new value: ");
        var newValue = Console.ReadLine();

        try
        {
            var doc = new XmlDocument();
            doc.Load(fileName);
            var node = doc.SelectSingleNode($"//Watches[{index}]/@{attrName}");
            if (node == null)
            {
                Console.WriteLine("Attribute not found.");
                return;
            }
            node.Value = newValue;
            doc.Save(fileName);
            Console.WriteLine("Modified.");
        }
        catch (Exception ex)
        {
            if (!ex.Message.Contains("expression must evaluate to a node-set"))
            {
                Console.WriteLine("Error");
            }
        }
    }

    /// <summary>
    /// Prints a list of watches.
    /// </summary>
    private void PrintWatches(List<Watches> watches)
    {
        foreach (var watch in watches)
        {
            watch.PrintObject();
        }
    }
}