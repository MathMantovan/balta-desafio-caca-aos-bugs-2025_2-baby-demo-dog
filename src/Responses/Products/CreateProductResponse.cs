namespace BugStore.Responses.Products
{
    public record CreateProductResponse(
        Guid Id,
        string Title,
        string Description,
        string Slug,
        decimal Price
    );
}
