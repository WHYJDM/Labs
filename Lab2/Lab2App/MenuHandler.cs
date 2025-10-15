using Lab2Library;

/// <summary>
/// Handles the main menu and user interactions.
/// </summary>
public class MenuHandler
{
    private readonly WatchManager _watchManager;
    private readonly XmlHandler _xmlHandler;
    private readonly string _fileName;
    private List<Watches> _watches;

    public MenuHandler(WatchManager watchManager, XmlHandler xmlHandler, string fileName)
    {
        _watchManager = watchManager;
        _xmlHandler = xmlHandler;
        _fileName = fileName;
        _watches = new List<Watches>();
    }

    /// <summary>
    /// Runs the main menu loop.
    /// </summary>
    public void Run()
    {
        while (true)
        {
            DisplayMenu();
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    _watches = _watchManager.CreateWatches();
                    _watchManager.PrintWatches(_watches);
                    break;
                case 2:
                    if (_watches.Count == 0)
                    {
                        Console.WriteLine("No watches to serialize. Create first.");
                        break;
                    }
                    _xmlHandler.SerializeWatches(_watches, _fileName);
                    break;
                case 3:
                    _xmlHandler.PrintFile(_fileName);
                    break;
                case 4:
                    _xmlHandler.DeserializeAndPrint(_fileName);
                    break;
                case 5:
                    _xmlHandler.FindModelsXDocument(_fileName);
                    break;
                case 6:
                    _xmlHandler.FindModelsXmlDocument(_fileName);
                    break;
                case 7:
                    _xmlHandler.ModifyAttributeXDocument(_fileName);
                    break;
                case 8:
                    _xmlHandler.ModifyAttributeXmlDocument(_fileName);
                    break;
                case 9:
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    /// <summary>
    /// Displays the menu options.
    /// </summary>
    private void DisplayMenu()
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
    }
}