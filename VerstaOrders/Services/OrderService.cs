using Microsoft.EntityFrameworkCore;
using VerstaOrders.Model;
using VerstaOrders.Model.Dto;

namespace VerstaOrders.Services;

public interface IOrderService
{
    IEnumerable<OrderDto> GetAllOrders();
    Task<OrderDto> GetOrder(string orderNumber);
    Task<OrderDto> CreateOrder(CreateOrderDto order);
    Task DeleteOrdersByYear(int year);
    Task ClearTestSequences(int year, int month);
}

public class OrderService(DataContext dataContext) : IOrderService
{
    public IEnumerable<OrderDto> GetAllOrders() => dataContext.Orders.OrderBy(o => o.PickupDate)
        .ThenBy(o => o.OrderNumber.Substring(o.OrderNumber.Length - 4)).Select(o => GetOrderDto(o));

    public async Task<OrderDto> GetOrder(string orderNumber)
    {
        var order = await dataContext.Orders.FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        if (order == null) throw new Exception($"Order with id {orderNumber} not found");
        return await Task.FromResult(GetOrderDto(order));
    }

    public async Task<OrderDto> CreateOrder(CreateOrderDto orderDto)
    {
        var newOrderNumber = GenerateOrderNumberMutex(orderDto);
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
        return await Task.FromResult(GetOrderDto(order));
    }
    
    public async Task DeleteOrdersByYear(int year)
    {
        var orders = dataContext.Orders.Where(o => o.PickupDate.Year == year);
        dataContext.Orders.RemoveRange(orders);
        await dataContext.SaveChangesAsync();
    }

    public async Task ClearTestSequences(int year, int month)
    {
        var foundSequences = dataContext.OrderDateSequences.Where(x => x.Year == year && x.Month == month); 
        dataContext.OrderDateSequences.RemoveRange(foundSequences);
        await dataContext.SaveChangesAsync();
    }

    private async Task<string> GenerateOrderNumber(CreateOrderDto order)
    {
        var foundOrderSequence = await dataContext.OrderDateSequences
            .FirstOrDefaultAsync(o => o.Year == order.PickupDate.Year && o.Month == order.PickupDate.Month);
        if (foundOrderSequence == null)
        {
            var orderSequence = new OrderDateSequence()
            {
                Month = order.PickupDate.Month,
                Year = order.PickupDate.Year,
                CurrentValue = 1
            };
            await dataContext.OrderDateSequences.AddAsync(orderSequence);
        }
        else
        {
            foundOrderSequence.CurrentValue++;
        }
        await dataContext.SaveChangesAsync();

        return new OrderNumberGenerator(order.PickupDate, foundOrderSequence?.CurrentValue ?? 1).Generate();
    }

    private static readonly Mutex _mutex = new Mutex();
    private string GenerateOrderNumberMutex(CreateOrderDto order)
    {
        var generatedOrderNumber = "";
        if (_mutex.WaitOne())
        {
            try
            {
                var foundOrderSequence = dataContext.OrderDateSequences
                    .FirstOrDefault(o => o.Year == order.PickupDate.Year && o.Month == order.PickupDate.Month);
                if (foundOrderSequence == null)
                {
                    var orderSequence = new OrderDateSequence()
                    {
                        Month = order.PickupDate.Month,
                        Year = order.PickupDate.Year,
                        CurrentValue = 1
                    };
                    dataContext.OrderDateSequences.Add(orderSequence);
                }
                else
                {
                    foundOrderSequence.CurrentValue++;
                }
                dataContext.SaveChanges();
                generatedOrderNumber = new OrderNumberGenerator(order.PickupDate, foundOrderSequence?.CurrentValue ?? 1).Generate();
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        return generatedOrderNumber;
    }

    private static OrderDto GetOrderDto(Order o) => 
        new(o.OrderNumber, 
            o.TownSender,
            o.AddressSender,
            o.TownReceiver,
            o.AddressReceiver,
            o.ProductWeight,
            o.PickupDate.ToUniversalTime());
}

public class OrderNumberGenerator(DateTime pickupDate, int number = 1)
{
    public string Generate() => $"ORD-{pickupDate.Date.Month:D2}{pickupDate.Date.Year}{number:D4}";
}