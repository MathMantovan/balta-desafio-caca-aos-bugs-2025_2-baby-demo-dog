namespace BugStore.Responses.Products
{
    public record GetProductResponse(
        Guid Id,
        string Title,
        string Description,
        string Slug,
        decimal Price
    );
}