using System;

namespace Lab7App;

/// <summary>
/// Handles the console menu for manufacturer operations.
/// </summary>
public class ManufacturerMenu
{
    private readonly Repository<Manufacturer> _manufacturerRepo;

    /// <summary>
    /// Initializes a new instance of the <see cref="ManufacturerMenu"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public ManufacturerMenu(ApplicationDbContext context)
    {
        _manufacturerRepo = new Repository<Manufacturer>(context);
    }

    /// <summary>
    /// Runs the manufacturer menu loop.
    /// </summary>
    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nManufacturer Menu:");
            Console.WriteLine("1. Add Manufacturer");
            Console.WriteLine("2. Get All Manufacturers");
            Console.WriteLine("3. Get Manufacturer by Id");
            Console.WriteLine("4. Update Manufacturer");
            Console.WriteLine("5. Delete Manufacturer");
            Console.WriteLine("0. Back");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddManufacturer();
                    break;
                case "2":
                    GetAllManufacturers();
                    break;
                case "3":
                    GetManufacturerById();
                    break;
                case "4":
                    UpdateManufacturer();
                    break;
                case "5":
                    DeleteManufacturer();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private void AddManufacturer()
    {
        Console.Write("Name: ");
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

        var manufacturer = Manufacturer.Create(name, address, isChild);
        _manufacturerRepo.Add(manufacturer);
        Console.WriteLine("Added.");
    }

    private void GetAllManufacturers()
    {
        foreach (var m in _manufacturerRepo.GetAll())
        {
            m.PrintObject();
        }
    }

    private void GetManufacturerById()
    {
        Console.Write("Id: ");
        var idInput = Console.ReadLine();
        if (!int.TryParse(idInput, out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var m = _manufacturerRepo.GetById(id);
        if (m != null) m.PrintObject();
        else Console.WriteLine("Not found.");
    }

    private void UpdateManufacturer()
    {
        Console.Write("Id: ");
        var idInput = Console.ReadLine();
        if (!int.TryParse(idInput, out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var m = _manufacturerRepo.GetById(id);
        if (m == null)
        {
            Console.WriteLine("Not found.");
            return;
        }

        Console.Write("Name: ");
        var name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name))
        {
            m.Name = name;
        }

        Console.Write("Address: ");
        var address = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(address))
        {
            m.Address = address;
        }

        Console.Write("Is Child Company (true/false): ");
        var isChildInput = Console.ReadLine();
        if (bool.TryParse(isChildInput, out var isChild))
        {
            m.IsAChildCompany = isChild;
        }
        else
        {
            Console.WriteLine("Invalid boolean value, keeping current.");
        }

        _manufacturerRepo.Update(m);
        Console.WriteLine("Updated.");
    }

    private void DeleteManufacturer()
    {
        Console.Write("Id: ");
        var idInput = Console.ReadLine();
        if (!int.TryParse(idInput, out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        _manufacturerRepo.Delete(id);
        Console.WriteLine("Deleted.");
    }
}