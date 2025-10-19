using System;

namespace Lab7App;

/// <summary>
/// Handles the console menu and user interactions for the application.
/// </summary>
public class ConsoleMenu
{
    private readonly ManufacturerMenu _manufacturerMenu;
    private readonly WatchMenu _watchMenu;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleMenu"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public ConsoleMenu(ApplicationDbContext context)
    {
        _manufacturerMenu = new ManufacturerMenu(context);
        _watchMenu = new WatchMenu(context);
    }

    /// <summary>
    /// Runs the main menu loop.
    /// </summary>
    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Manufacturer Menu");
            Console.WriteLine("2. Watch Menu");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    _manufacturerMenu.Run();
                    break;
                case "2":
                    _watchMenu.Run();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }


}