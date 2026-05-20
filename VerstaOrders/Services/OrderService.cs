using VerstaOrders.Model;

namespace VerstaOrders.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrders();
    Task<Order> GetOrder(Guid orderId);
    Task<Order> CreateOrder(Order order);
}

public class OrderService(DataContext dataContext) : IOrderService
{
    public Task<IEnumerable<Order>> GetAllOrders() => Task.FromResult(dataContext.Orders.AsEnumerable());

    public async Task<Order> GetOrder(Guid orderId)
    {
        var order = await dataContext.Orders.FindAsync(orderId);
        if (order == null) throw new Exception($"Order with id {orderId} not found");
        return await Task.FromResult(order);
    }

    public async Task<Order> CreateOrder(Order order) 
    {
        order = order with { OrderId = Guid.NewGuid() };
        dataContext.Orders.Add(order);
        await dataContext.SaveChangesAsync();
        return await Task.FromResult(order);
    }
}