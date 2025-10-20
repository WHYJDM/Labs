using Lab8TPH;
using Lab8Models;

Console.WriteLine("TPH Strategy Demo");

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

    // Add watches
    var electronicWatch = ElectronicWatches.Create("G-Shock", "GS123", manufacturer2.Id, 24, true);
    var mechanicWatch = MechanicWatches.Create("Submariner", "SM456", manufacturer1.Id, "Automatic", 25);
    var towerWatch = TowerWatches.Create("Big Ben", "BB789", manufacturer1.Id, 96.0, "London");

    context.ElectronicWatches.Add(electronicWatch);
    context.MechanicWatches.Add(mechanicWatch);
    context.TowerWatches.Add(towerWatch);
    context.SaveChanges();

    Console.WriteLine("Data added successfully.");
}

using (var context = new ApplicationDbContext())
{
    var allWatches = context.Watches.ToList();
    Console.WriteLine("All Watches:");
    foreach (var watch in allWatches)
    {
        watch.PrintObject();
        Console.WriteLine();
    }

    var electronicWatches = context.ElectronicWatches.ToList();
    Console.WriteLine("Electronic Watches:");
    foreach (var watch in electronicWatches)
    {
        watch.PrintObject();
        Console.WriteLine();
    }
}

Console.WriteLine("TPH Demo completed.");
