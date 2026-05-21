using System.Globalization;
using Microsoft.EntityFrameworkCore;
using VerstaOrders.Model;

namespace VerstaOrders.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrders();
    Task<Order> GetOrder(Guid orderId);
    Task<Order> CreateOrder(CreateOrderDto order);
}

public class OrderService(DataContext dataContext) : IOrderService
{
    public Task<IEnumerable<Order>> GetAllOrders() 
        => Task.FromResult(dataContext.Orders.OrderBy(o => o.OrderNumber.Substring(0, 8))
            .ThenBy(o => o.OrderNumber.Substring(o.OrderNumber.Length - 4)).AsEnumerable());

    public async Task<Order> GetOrder(Guid orderId)
    {
        var order = await dataContext.Orders.FindAsync(orderId);
        if (order == null) throw new Exception($"Order with id {orderId} not found");
        return await Task.FromResult(order);
    }

    public async Task<Order> CreateOrder(CreateOrderDto orderDto)
    {
        var newOrderNumber = await GenerateOrderNumber(orderDto);
        if (newOrderNumber == null) throw new Exception("Order number was not created");
        var order = new Order(
            Guid.NewGuid(),
            newOrderNumber,
            orderDto.TownSender,
            orderDto.AddressSender,
            orderDto.TownReceiver,
            orderDto.AddressReceiver,
            orderDto.ProductWeight,
            orderDto.PickupDate.ToUniversalTime());
        dataContext.Orders.Add(order);
        await dataContext.SaveChangesAsync();
        return await Task.FromResult(order);
    }

    private async Task<string> GenerateOrderNumber(CreateOrderDto order)
    {
        var lastOrder = await dataContext.Orders.OrderBy(o => o.OrderNumber.Substring(o.OrderNumber.Length - 4)).LastOrDefaultAsync();
        return lastOrder == null 
            ? new OrderNumberGenerator(null).Generate(order.PickupDate)
            : new OrderNumberGenerator(lastOrder.OrderNumber).Generate(order.PickupDate);
    }
}

public class OrderNumberGenerator
{
    private readonly DateTime? _date;
    private readonly int _number;
    public OrderNumberGenerator(string? orderNumber)
    {
        if (orderNumber == null) return;
        var cutOrd = orderNumber.Replace("ORD-", "");
        var dateStr = cutOrd.Substring(0, 8);
        _date = DateTime.ParseExact(dateStr, "ddMMyyyy", CultureInfo.InvariantCulture);
        var numberStr = cutOrd.Substring(cutOrd.Length - 4);
        _number = int.Parse(numberStr);
    }

    public string Generate(DateTime newDate)
    {
        if (_date == null) return CreateNumber(newDate, 1);
        var datesEquals = newDate.Date == _date?.Date;
        return CreateNumber(newDate, datesEquals ? _number + 1 : 1);
    }
    private string CreateNumber(DateTime date, int number)
        => $"ORD-{date.Date.Day:D2}{date.Date.Month:D2}{date.Date.Year}{number:D4}";
}