using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Requests.Products
{
    public record GetProductRequest() : IRequest<IEnumerable<GetProductResponse>>;
}