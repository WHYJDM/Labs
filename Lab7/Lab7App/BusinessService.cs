using System;
using Microsoft.EntityFrameworkCore;

namespace Lab7App;

/// <summary>
/// Provides business logic operations for the application.
/// </summary>
public class BusinessService
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessService"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public BusinessService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new product for a new manufacturer in a transactional manner.
    /// </summary>
    /// <param name="manufacturerName">The name of the manufacturer.</param>
    /// <param name="address">The address of the manufacturer.</param>
    /// <param name="isChild">Indicates if the manufacturer is a child company.</param>
    /// <param name="model">The model of the watch.</param>
    /// <param name="serialNumber">The serial number of the watch.</param>
    /// <param name="type">The type of the watch.</param>
    public void AddNewProductForNewManufacturer(string manufacturerName, string address, bool isChild, string model, string serialNumber, WatchesType type)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var manufacturer = new Manufacturer { Name = manufacturerName, Address = address, IsAChildCompany = isChild };
            _context.Manufacturers.Add(manufacturer);
            _context.SaveChanges();

            var watch = new Watches { Model = model, SerialNumber = serialNumber, Type = type, ManufacturerId = manufacturer.Id };
            _context.Watches.Add(watch);
            _context.SaveChanges();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}