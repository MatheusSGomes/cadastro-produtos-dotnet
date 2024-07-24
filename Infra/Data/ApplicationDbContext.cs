using IWantApp.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace IWantApp.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .IsRequired();

        // Gerou erro
        // modelBuilder.Entity<Product>()
        //     .Property(p => p.Category)
        //     .IsRequired();
        
        modelBuilder.Entity<Product>()
            .Property(p => p.Description)
            .HasMaxLength(255)
            .IsRequired(false);

        // Não necessário se já estiver mapeado corretamente
        modelBuilder.Entity<Category>()
            .ToTable("Categories");
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<string>()
            .HaveMaxLength(100);
    }
}