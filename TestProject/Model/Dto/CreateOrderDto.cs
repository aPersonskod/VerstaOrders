namespace TestProject.Model.Dto;

public record CreateOrderDto(
    string TownSender,
    string AddressSender,
    string TownReceiver,
    string AddressReceiver,
    decimal ProductWeight,
    DateTime PickupDate);