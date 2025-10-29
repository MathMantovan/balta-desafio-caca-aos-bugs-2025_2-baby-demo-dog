namespace BugStore.Responses.Orders
{
    public record GetOrderByIdResponse(
        Guid Id,
        Guid CustomerId,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        List<GetOrderLineResponse> Lines
    );

    public record GetOrderLineResponse(
        Guid ProductId,
        string ProductTitle,
        int Quantity,
        decimal Total
    );
}
