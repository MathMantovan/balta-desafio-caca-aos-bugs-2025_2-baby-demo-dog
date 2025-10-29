using BugStore.Data;
using BugStore.Requests.Products;
using BugStore.Responses.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Products
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, GetProductByIdResponse?>
    {
        private readonly AppDbContext _dbContext;

        public GetProductByIdHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetProductByIdResponse?> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Id == request.Id)
                .Select(p => new GetProductByIdResponse(
                    p.Id,
                    p.Title,
                    p.Description,
                    p.Slug,
                    p.Price
                ))
                .FirstOrDefaultAsync(cancellationToken);

            return product;
        }
    }
}