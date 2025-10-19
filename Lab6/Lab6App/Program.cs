using Lab6App;

var db = new DatabaseManager();

// Task 0: Create tables
db.CreateTables();
Console.WriteLine("Tables created.");

// Task 1: Fill data asynchronously
await db.FillDataAsync();
Console.WriteLine("Data filled.");

// Task 2: Add new records
var newMan = Manufacturer.Create("New Manufacturer", "New Address", true);
var manId = await db.AddManufacturerAsync(newMan);
Console.WriteLine($"New manufacturer added with Id: {manId}");

var newWatch = Watches.Create("New Model", "NewSN", WatchesType.Electronic, manId);
await db.AddWatchesAsync(newWatch);
Console.WriteLine("New watch added.");

// Task 3: Get watches for manufacturer
var watches = await db.GetWatchesByManufacturerAsync(1);
Console.WriteLine($"Watches for manufacturer 1:");
foreach (var w in watches)
{
    w.PrintObject();
}
