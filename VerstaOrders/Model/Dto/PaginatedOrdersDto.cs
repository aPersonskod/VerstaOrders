namespace VerstaOrders.Model.Dto;

public record PaginatedOrdersDto(int RecordsCount, int CurrentPage, int PageSize, IEnumerable<OrderDto> Orders);