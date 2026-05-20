using Microsoft.EntityFrameworkCore;
using VerstaOrders.Model;

namespace VerstaOrders;

public sealed class DataContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }
}