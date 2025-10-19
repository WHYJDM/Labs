using System;
using System.Linq;

namespace Lab7App;

/// <summary>
/// Provides data initialization for the application database.
/// </summary>
public static class DataInitializer
{
    /// <summary>
    /// Initializes the database with sample data if it is empty.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Manufacturers.Any() || context.Watches.Any())
        {
            return;
        }

        var manufacturers = new List<Manufacturer>();
        for (int i = 0; i < 30; i++)
        {
            manufacturers.Add(Manufacturer.Create($"Manufacturer {i + 1}", $"Address {i + 1}", i % 2 == 0));
        }
        context.Manufacturers.AddRange(manufacturers);
        context.SaveChanges();

        var watches = new List<Watches>();
        var random = new Random();
        for (int i = 0; i < 30; i++)
        {
            var type = (WatchesType)random.Next(0, 3);
            watches.Add(Watches.Create($"Model {i + 1}", $"SN{i + 1:D6}", type, manufacturers[i % 30].Id));
        }
        context.Watches.AddRange(watches);
        context.SaveChanges();
    }
}