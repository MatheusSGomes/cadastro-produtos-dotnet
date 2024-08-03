using IWantApp.Domain.Orders;

namespace IWantApp.Infra.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<Notification>();
 
        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .IsRequired();
        
        modelBuilder.Entity<Product>()
            .Property(p => p.Description)
            .HasMaxLength(255)
            .IsRequired(false);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        modelBuilder.Entity<Category>()
            .ToTable("Categories");

        modelBuilder.Entity<Order>()
            .Property(order => order.ClientId)
            .IsRequired();

        modelBuilder.Entity<Order>()
            .Property(order => order.DeliveryAddress)
            .IsRequired();

        // Relacionamento. 1 pedido (order) -> tem muitos -> produtos
        modelBuilder.Entity<Order>()
            .HasMany(order => order.Products)
            .WithMany(product => product.Orders)
            .UsingEntity(x => x.ToTable("OrderProducts"));
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<string>()
            .HaveMaxLength(100);
    }
}
