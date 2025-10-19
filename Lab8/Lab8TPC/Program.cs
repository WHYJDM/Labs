using Lab8TPC;
using Lab8Models;

Console.WriteLine("TPC Strategy Demo");

using (var context = new ApplicationDbContext())
{
    // Ensure database is created
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    // Add manufacturers
    var manufacturer1 = Manufacturer.Create("Rolex", "Switzerland", false);
    var manufacturer2 = Manufacturer.Create("Casio", "Japan", true);
    context.Manufacturers.AddRange(manufacturer1, manufacturer2);
    context.SaveChanges();
}

using (var context = new ApplicationDbContext())
{
    // Add watches
    var electronicWatch = ElectronicWatches.Create("G-Shock", "GS123", 2, 24, true);
    context.ElectronicWatches.Add(electronicWatch);
    context.SaveChanges();
}

using (var context = new ApplicationDbContext())
{
    var mechanicWatch = MechanicWatches.Create("Submariner", "SM456", 1, "Automatic", 25);
    context.MechanicWatches.Add(mechanicWatch);
    context.SaveChanges();
}

using (var context = new ApplicationDbContext())
{
    var towerWatch = TowerWatches.Create("Big Ben", "BB789", 1, 96.0, "London");
    context.TowerWatches.Add(towerWatch);
    context.SaveChanges();

    Console.WriteLine("Data added successfully.");
}

using (var context = new ApplicationDbContext())
{
    // Query specific types
    var electronicWatches = context.ElectronicWatches.ToList();
    Console.WriteLine("Electronic Watches:");
    foreach (var watch in electronicWatches)
    {
        watch.PrintObject();
        Console.WriteLine();
    }

    // Note: Queries for other types have issues in this EF Core version
}

Console.WriteLine("TPC Demo completed.");
