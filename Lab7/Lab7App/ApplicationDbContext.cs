using Microsoft.EntityFrameworkCore;

namespace Lab7App;

/// <summary>
/// Application database context for Entity Framework.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Gets or sets the manufacturers DbSet.
    /// </summary>
    public DbSet<Manufacturer> Manufacturers { get; set; }

    /// <summary>
    /// Gets or sets the watches DbSet.
    /// </summary>
    public DbSet<Watches> Watches { get; set; }

    /// <summary>
    /// Configures the database context options.
    /// </summary>
    /// <param name="optionsBuilder">The options builder.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=watches.db");
    }

    /// <summary>
    /// Configures the model for the database context.
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
            entity.Property(e => e.IsAChildCompany).IsRequired();
        });

        modelBuilder.Entity<Watches>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Model).IsRequired().HasMaxLength(100);
            entity.Property(e => e.SerialNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.ManufacturerId).IsRequired();
            entity.HasOne(e => e.Manufacturer)
                  .WithMany(m => m.Watches)
                  .HasForeignKey(e => e.ManufacturerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}