using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Lab2Library;


var watches = new List<Watches>();
var fileName = "watches.xml";
var serializer = new XmlSerializer(typeof(List<Watches>));

while (true)
{
    Console.WriteLine("\nMenu:");
    Console.WriteLine("1. Create 10 instances and print");
    Console.WriteLine("2. Serialize to XML");
    Console.WriteLine("3. Print file content");
    Console.WriteLine("4. Deserialize and print");
    Console.WriteLine("5. Find Model values using XDocument");
    Console.WriteLine("6. Find Model values using XmlDocument");
    Console.WriteLine("7. Modify attribute using XDocument");
    Console.WriteLine("8. Modify attribute using XmlDocument");
    Console.WriteLine("9. Exit");
    Console.Write("Choose option: ");

    if (!int.TryParse(Console.ReadLine(), out int choice))
    {
        Console.WriteLine("Invalid input.");
        continue;
    }

    switch (choice)
    {
        case 1:
            watches = CreateWatches();
            PrintWatches(watches);
            break;
        case 2:
            if (watches.Count == 0)
            {
                Console.WriteLine("No watches to serialize. Create first.");
                break;
            }
            SerializeWatches(watches, fileName, serializer);
            break;
        case 3:
            PrintFile(fileName);
            break;
        case 4:
            DeserializeAndPrint(fileName, serializer);
            break;
        case 5:
            FindModelsXDocument(fileName);
            break;
        case 6:
            FindModelsXmlDocument(fileName);
            break;
        case 7:
            ModifyAttributeXDocument(fileName);
            break;
        case 8:
            ModifyAttributeXmlDocument(fileName);
            break;
        case 9:
            return;
        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}
static List<Watches> CreateWatches()
{
    var watches = new List<Watches>();
    for (var i = 1; i <= 10; i++)
    {
        watches.Add(Watches.Create(
            i,
            $"Model{i}",
            $"SN{i}",
            (WatchesType)((i - 1) % 3)));
    }
    return watches;
}

static void PrintWatches(List<Watches> watches)
{
    foreach (var watch in watches)
    {
        watch.PrintObject();
    }
}

static void SerializeWatches(List<Watches> watches, string fileName, XmlSerializer serializer)
{
    try
    {
        using var writer = new StreamWriter(fileName);
        serializer.Serialize(writer, watches);
        Console.WriteLine("Serialized to " + fileName);
    }
    catch
    {
        Console.WriteLine("Error");
    }
}

static void PrintFile(string fileName)
{
    if (!File.Exists(fileName))
    {
        Console.WriteLine("File does not exist.");
        return;
    }
    Console.WriteLine(File.ReadAllText(fileName));
}

static void DeserializeAndPrint(string fileName, XmlSerializer serializer)
{
    if (!File.Exists(fileName))
    {
        Console.WriteLine("File does not exist.");
        return;
    }
    try
    {
        using var reader = new StreamReader(fileName);
        var deserialized = (List<Watches>)serializer.Deserialize(reader);
        PrintWatches(deserialized);
    }
    catch
    {
        Console.WriteLine("Error");
    }
}

static void FindModelsXDocument(string fileName)
{
    if (!File.Exists(fileName))
    {
        Console.WriteLine("File does not exist.");
        return;
    }
    try
    {
        var doc = XDocument.Load(fileName);
        var models = doc.Descendants("Watches")
            .Attributes("Model")
            .Select(a => a.Value);
        Console.WriteLine("Models: " + string.Join(", ", models) + ",");
    }
    catch
    {
        Console.WriteLine("Error");
    }
}

static void FindModelsXmlDocument(string fileName)
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
        var nodes = doc.SelectNodes("//Watches/@Model");
        Console.Write("Models: ");
        foreach (XmlAttribute attr in nodes)
        {
            Console.Write(attr.Value + ", ");
        }
        Console.WriteLine();
    }
    catch
    {
        Console.WriteLine("Error");
    }
}

static void ModifyAttributeXDocument(string fileName)
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

static void ModifyAttributeXmlDocument(string fileName)
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
