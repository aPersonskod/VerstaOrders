namespace VerstaOrders.Model;

public record CreateOrderDto(
    string TownSender,
    string AddressSender,
    string TownReceiver,
    string AddressReceiver,
    double ProductWeight,
    DateTime PickupDate);