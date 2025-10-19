using System;

namespace Lab7App;

/// <summary>
/// Main program class for the Lab7 application.
/// </summary>
class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    static void Main(string[] args)
    {
        using var context = new ApplicationDbContext();
        DataInitializer.Initialize(context);

        var menu = new ConsoleMenu(context);
        menu.Run();
    }
}
