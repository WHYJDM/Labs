using System;

namespace Lab7App;

/// <summary>
/// Handles the console menu for watch operations.
/// </summary>
public class WatchMenu
{
    private readonly Repository<Watches> _watchesRepo;
    private readonly BusinessService _businessService;
    private readonly QueryService _queryService;

    /// <summary>
    /// Initializes a new instance of the <see cref="WatchMenu"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public WatchMenu(ApplicationDbContext context)
    {
        _watchesRepo = new Repository<Watches>(context);
        _businessService = new BusinessService(context);
        _queryService = new QueryService(context);
    }

    /// <summary>
    /// Runs the watch menu loop.
    /// </summary>
    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nWatch Menu:");
            Console.WriteLine("1. Add Watch");
            Console.WriteLine("2. Get All Watches");
            Console.WriteLine("3. Get Watch by Id");
            Console.WriteLine("4. Update Watch");
            Console.WriteLine("5. Delete Watch");
            Console.WriteLine("6. Add New Product for New Manufacturer");
            Console.WriteLine("7. Get Watches by Manufacturer");
            Console.WriteLine("0. Back");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddWatch();
                    break;
                case "2":
                    GetAllWatches();
                    break;
                case "3":
                    GetWatchById();
                    break;
                case "4":
                    UpdateWatch();
                    break;
                case "5":
                    DeleteWatch();
                    break;
                case "6":
                    AddNewProductForNewManufacturer();
                    break;
                case "7":
                    GetWatchesByManufacturer();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private void AddWatch()
    {
        Console.Write("Model: ");
        var model = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(model))
        {
            Console.WriteLine("Model cannot be empty.");
            return;
        }

        Console.Write("Serial Number: ");
        var sn = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(sn))
        {
            Console.WriteLine("Serial Number cannot be empty.");
            return;
        }

        Console.Write("Type (Electronic/Mechanic/Tower): ");
        var typeInput = Console.ReadLine();
        if (!Enum.TryParse<WatchesType>(typeInput, out var type))
        {
            Console.WriteLine("Invalid type.");
            return;
        }

        Console.Write("Manufacturer Id: ");
        var midInput = Console.ReadLine();
        if (!int.TryParse(midInput, out var mid))
        {
            Console.WriteLine("Invalid Manufacturer ID.");
            return;
        }

        var watch = Watches.Create(model, sn, type, mid);
        _watchesRepo.Add(watch);
        Console.WriteLine("Added.");
    }

    private void GetAllWatches()
    {
        foreach (var w in _watchesRepo.GetAll())
        {
            w.PrintObject();
        }
    }

    private void GetWatchById()
    {
        Console.Write("Id: ");
        var idInput = Console.ReadLine();
        if (!int.TryParse(idInput, out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var w = _watchesRepo.GetById(id);
        if (w != null) w.PrintObject();
        else Console.WriteLine("Not found.");
    }

    private void UpdateWatch()
    {
        Console.Write("Id: ");
        var idInput = Console.ReadLine();
        if (!int.TryParse(idInput, out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var w = _watchesRepo.GetById(id);
        if (w == null)
        {
            Console.WriteLine("Not found.");
            return;
        }

        Console.Write("Model: ");
        var model = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(model))
        {
            w.Model = model;
        }

        Console.Write("Serial Number: ");
        var sn = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(sn))
        {
            w.SerialNumber = sn;
        }

        Console.Write("Type (Electronic/Mechanic/Tower): ");
        var typeInput = Console.ReadLine();
        if (Enum.TryParse<WatchesType>(typeInput, out var type))
        {
            w.Type = type;
        }
        else
        {
            Console.WriteLine("Invalid type, keeping current.");
        }

        Console.Write("Manufacturer Id: ");
        var midInput = Console.ReadLine();
        if (int.TryParse(midInput, out var mid))
        {
            w.ManufacturerId = mid;
        }
        else
        {
            Console.WriteLine("Invalid Manufacturer ID, keeping current.");
        }

        _watchesRepo.Update(w);
        Console.WriteLine("Updated.");
    }

    private void DeleteWatch()
    {
        Console.Write("Id: ");
        var idInput = Console.ReadLine();
        if (!int.TryParse(idInput, out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        _watchesRepo.Delete(id);
        Console.WriteLine("Deleted.");
    }

    private void AddNewProductForNewManufacturer()
    {
        Console.Write("Manufacturer Name: ");
        var name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be empty.");
            return;
        }

        Console.Write("Address: ");
        var address = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(address))
        {
            Console.WriteLine("Address cannot be empty.");
            return;
        }

        Console.Write("Is Child Company (true/false): ");
        var isChildInput = Console.ReadLine();
        if (!bool.TryParse(isChildInput, out var isChild))
        {
            Console.WriteLine("Invalid boolean value.");
            return;
        }

        Console.Write("Watch Model: ");
        var model = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(model))
        {
            Console.WriteLine("Model cannot be empty.");
            return;
        }

        Console.Write("Serial Number: ");
        var sn = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(sn))
        {
            Console.WriteLine("Serial Number cannot be empty.");
            return;
        }

        Console.Write("Type (Electronic/Mechanic/Tower): ");
        var typeInput = Console.ReadLine();
        if (!Enum.TryParse<WatchesType>(typeInput, out var type))
        {
            Console.WriteLine("Invalid type.");
            return;
        }

        try
        {
            _businessService.AddNewProductForNewManufacturer(name, address, isChild, model, sn, type);
            Console.WriteLine("Added successfully.");
        }
        catch
        {
            Console.WriteLine("Failed to add.");
        }
    }

    private void GetWatchesByManufacturer()
    {
        Console.Write("Manufacturer Id: ");
        var idInput = Console.ReadLine();
        if (!int.TryParse(idInput, out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        foreach (var w in _queryService.GetWatchesByManufacturer(id))
        {
            w.PrintObject();
        }
    }
}