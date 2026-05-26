namespace VerstaOrders.Model;

public record Order(
    Guid OrderId,
    string OrderNumber,
    string TownSender,
    string AddressSender,
    string TownReceiver,
    string AddressReceiver,
    double ProductWeight,
    DateTime PickupDate);