using TestProject.Ext;
using VerstaOrders.Model.Dto;

namespace TestProject;

public class Tests
{
    private List<CreateOrderDto> _orders = [];
    private const int tYear = 2019; 
    private const int m1 = 2; 
    private const int m2 = 3; 
    private const int m3 = 4; 
    
    [SetUp]
    public void Setup()
    {
        for (var i = 1; i <= 5; i++)
        {
            _orders.Add(new CreateOrderDto($"ts{i}", $"as{i}", $"tr{i}", $"ar{i}", i, new DateTime(tYear, m1, i + 1)));
            _orders.Add(new CreateOrderDto($"ts{i}", $"as{i}", $"tr{i}", $"ar{i}", i, new DateTime(tYear, m2, i + 1)));
            _orders.Add(new CreateOrderDto($"ts{i}", $"as{i}", $"tr{i}", $"ar{i}", i, new DateTime(tYear, m3, i + 1)));
        }
    }

    [Test]
    public void Test1()
    {
        var createdOrders = new List<OrderDto>();
        const string baseAddress = "http://localhost:5259/Order";
        var task = Task.Run(async () =>
        {
            var res = await CheckOrdersCreating(baseAddress);
            createdOrders.AddRange(res);
        });
        task.Wait();
        
        // check orderNumber creates true
        Assert.That(createdOrders, Is.Not.Empty);
        for (var i = 1; i <= 5; i++)
        {
            Assert.That(createdOrders.FirstOrDefault(x => x.OrderNumber == $"ORD-{m1:D2}{tYear}000{i}"), Is.Not.Null);
            Assert.That(createdOrders.FirstOrDefault(x => x.OrderNumber == $"ORD-{m2:D2}{tYear}000{i}"), Is.Not.Null);
            Assert.That(createdOrders.FirstOrDefault(x => x.OrderNumber == $"ORD-{m3:D2}{tYear}000{i}"), Is.Not.Null);
        }

        task = Task.Run(async () => await CleanTestData(baseAddress));
        task.Wait();
    }

    private async Task<IEnumerable<OrderDto>> CheckOrdersCreating(string baseAddress)
    {
        await CleanTestData(baseAddress);

        // create orders
        foreach (var o in _orders)
        {
            await $"{baseAddress}".PostQuery(o);
        }

        // get all orders
        var createdOrders = await $"{baseAddress}/GetOrders".GetQuery<IEnumerable<OrderDto>>();
        return createdOrders;
    }

    private async Task CleanTestData(string baseAddress)
    {
        // try to remove unused orders
        for (var i = 0; i <= 5; i++) await $"{baseAddress}/ts{i}".DeleteQuery();
        await $"{baseAddress}/ClearTestSequences?year={tYear}&month={m1}".PostQuery();
        await $"{baseAddress}/ClearTestSequences?year={tYear}&month={m2}".PostQuery();
        await $"{baseAddress}/ClearTestSequences?year={tYear}&month={m3}".PostQuery();
    }


}