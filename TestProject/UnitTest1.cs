using TestProject.Ext;
using VerstaOrders.Model.Dto;

namespace TestProject;

public class Tests
{
    private List<CreateOrderDto> _orders = [];
    private const int tYear = 2011; 
    private const int m1 = 2; 
    private const int m2 = 3; 
    private const int m3 = 4; 
    private const int itemCount = 20;
    
    private readonly List<string> Cities =
    [
        "Москва",
        "Санкт-Петербург",
        "Новосибирск",
        "Екатеринбург",
        "Казань",
        "Нижний Новгород",
        "Челябинск",
        "Омск",
        "Ростов-на-Дону",
        "Владивосток"
    ];
    
    private readonly List<string> Addresses =
    [
        "ул. Ленина, д. 15",
        "ул. Пушкина, д. 8, кв. 27",
        "ул. Советская, д. 42",
        "пр. Мира, д. 101",
        "ул. Гагарина, д. 33, стр. 2",
        "ул. Кирова, д. 56а",
        "ул. Молодёжная, д. 7",
        "ул. Октябрьская, д. 84",
        "ул. Садовая, д. 12",
        "ул. Лермонтова, д. 23, кв. 45",
        "ул. Некрасова, д. 5",
        "ул. 8 Марта, д. 19",
        "ул. Победы, д. 62",
        "ул. Строителей, д. 9, корп. 1",
        "ул. Московская, д. 74",
        "ул. Чапаева, д. 31",
        "ул. Энтузиастов, д. 108",
        "ул. Дзержинского, д. 47",
        "ул. Ленинградская, д. 3",
        "ул. Космонавтов, д. 88"
    ];
    
    private string RC() => Cities[new Random().Next(0, Cities.Count - 1)];
    private string RA() => Addresses[new Random().Next(0, Addresses.Count - 1)];
    private int RD() => new Random().Next(1, 28);
    
    [SetUp]
    public void Setup()
    {
        for (var i = 1; i <= itemCount; i++)
        {
            _orders.Add(new CreateOrderDto(RC(), RA(), RC(), RA(), i, new DateTime(tYear, m1, RD())));
            _orders.Add(new CreateOrderDto(RC(), RA(), RC(), RA(), i, new DateTime(tYear, m2, RD())));
            _orders.Add(new CreateOrderDto(RC(), RA(), RC(), RA(), i, new DateTime(tYear, m3, RD())));
        }
    }

    [Test]
    public void ConcurrentTest()
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
        for (var i = 1; i <= itemCount; i++)
        {
            Assert.That(createdOrders.FirstOrDefault(x => x.OrderNumber == $"ORD-{m1:D2}{tYear}{i:D4}"), Is.Not.Null);
            Assert.That(createdOrders.FirstOrDefault(x => x.OrderNumber == $"ORD-{m2:D2}{tYear}{i:D4}"), Is.Not.Null);
            Assert.That(createdOrders.FirstOrDefault(x => x.OrderNumber == $"ORD-{m3:D2}{tYear}{i:D4}"), Is.Not.Null);
        }

        var task2 = Task.Run(async () => await CleanTestData(baseAddress));
        task2.Wait();
    }

    private async Task<IEnumerable<OrderDto>> CheckOrdersCreating(string baseAddress)
    {
        await CleanTestData(baseAddress);

        // create orders
        var options = new ParallelOptions() { MaxDegreeOfParallelism = 20 };
        Parallel.ForEach(_orders, options, order =>
        {
            var task = $"{baseAddress}".PostQuery(order);
            task.Wait();
        });

        // get all orders
        var createdOrders = await $"{baseAddress}/GetOrders".GetQuery<IEnumerable<OrderDto>>();
        return createdOrders;
    }

    private async Task CleanTestData(string baseAddress)
    {
        // try to remove unused orders
        await $"{baseAddress}/{tYear}".DeleteQuery();
        await $"{baseAddress}/ClearTestSequences?year={tYear}&month={m1}".PostQuery();
        await $"{baseAddress}/ClearTestSequences?year={tYear}&month={m2}".PostQuery();
        await $"{baseAddress}/ClearTestSequences?year={tYear}&month={m3}".PostQuery();
    }
}