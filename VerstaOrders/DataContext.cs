using Microsoft.EntityFrameworkCore;
using VerstaOrders.Model;

namespace VerstaOrders;

public sealed class DataContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDateSequence> OrderDateSequences { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().HasData(
            new Order(new Guid("a582d854-2216-42b7-95b9-c9b2c2d7d669"), "ORD-0520260001", "Москва", "ул. Тверская, д. 10", "Санкт-Петербург", "Невский пр., д. 25", 15.5, new DateTime(2026, 05, 22).ToUniversalTime()),
            new Order(new Guid("5c5759c6-4b4e-4150-bbe0-912c1c8f8c34"), "ORD-0520260002", "Казань", "ул. Баумана, 5", "Екатеринбург", "пр. Ленина, 100", 8.2, new DateTime(2026, 05, 22).ToUniversalTime()),
            new Order(new Guid("9288547b-e8f0-4d05-add5-5f29a4a3f94b"), "ORD-0520260003", "Новосибирск", "Красный пр., 12", "Владивосток", "Океанский пр., 7", 42, new DateTime(2026, 05, 22).ToUniversalTime())
        );

        modelBuilder.Entity<OrderDateSequence>().HasData(
            new OrderDateSequence {Id = 1, Month = 5, Year = 2026, CurrentValue = 3}
        );
    }
}