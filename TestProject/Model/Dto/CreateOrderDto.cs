namespace VerstaOrders.Model.Dto;

public record CreateOrderDto(
    string TownSender,
    string AddressSender,
    string TownReceiver,
    string AddressReceiver,
    double ProductWeight,
    DateTime PickupDate);