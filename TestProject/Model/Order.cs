namespace TestProject.Model;

public record Order(
    Guid OrderId,
    string OrderNumber,
    string TownSender,
    string AddressSender,
    string TownReceiver,
    string AddressReceiver,
    decimal ProductWeight,
    DateTime PickupDate);