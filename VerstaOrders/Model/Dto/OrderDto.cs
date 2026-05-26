namespace VerstaOrders.Model.Dto;

public record OrderDto(
    string OrderNumber,
    string TownSender,
    string AddressSender,
    string TownReceiver,
    string AddressReceiver,
    double ProductWeight,
    DateTime PickupDate);