using Microsoft.EntityFrameworkCore;
using Lab8Models;

namespace Lab8TPH;

/// <summary>
/// Application database context for TPH strategy.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Gets or sets the manufacturers.
    /// </summary>
    public DbSet<Manufacturer> Manufacturers { get; set; }

    /// <summary>
    /// Gets or sets the watches (base class).
    /// </summary>
    public DbSet<Watches> Watches { get; set; }

    /// <summary>
    /// Gets or sets the electronic watches.
    /// </summary>
    public DbSet<ElectronicWatches> ElectronicWatches { get; set; }

    /// <summary>
    /// Gets or sets the mechanic watches.
    /// </summary>
    public DbSet<MechanicWatches> MechanicWatches { get; set; }

    /// <summary>
    /// Gets or sets the tower watches.
    /// </summary>
    public DbSet<TowerWatches> TowerWatches { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    public ApplicationDbContext()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Configures the model.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TPH Configuration
        modelBuilder.Entity<Watches>()
            .UseTphMappingStrategy()
            .HasDiscriminator<string>("WatchType")
            .HasValue<ElectronicWatches>("Electronic")
            .HasValue<MechanicWatches>("Mechanic")
            .HasValue<TowerWatches>("Tower");
    }

    /// <summary>
    /// Configures the database.
    /// </summary>
    /// <param name="optionsBuilder">The options builder.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=lab8tph.db");
        }
    }
}