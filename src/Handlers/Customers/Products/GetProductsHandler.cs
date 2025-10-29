using BugStore.Data;
using BugStore.Requests.Products;
using BugStore.Responses.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Products
{
    public class GetProductHandler : IRequestHandler<GetProductRequest, IEnumerable<GetProductResponse>>
    {
        private readonly AppDbContext _dbContext;

        public GetProductHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<GetProductResponse>> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.Products
                .AsNoTracking()
                .Select(x => new GetProductResponse(
                    x.Id,
                    x.Title,
                    x.Description,
                    x.Slug,
                    x.Price
                ))
                .ToListAsync(cancellationToken);

            return products;
        }
    }
}
