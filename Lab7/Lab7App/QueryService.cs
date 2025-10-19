using System.Collections.Generic;
using System.Linq;

namespace Lab7App;

/// <summary>
/// Provides query operations for the application.
/// </summary>
public class QueryService
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="QueryService"/> class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public QueryService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all watches associated with a specific manufacturer.
    /// </summary>
    /// <param name="manufacturerId">The ID of the manufacturer.</param>
    /// <returns>An enumerable collection of watches.</returns>
    public IEnumerable<Watches> GetWatchesByManufacturer(int manufacturerId) // ВСЕ часы от 1 производителя
    {
        return _context.Watches.Where(w => w.ManufacturerId == manufacturerId).ToList();
    }
}