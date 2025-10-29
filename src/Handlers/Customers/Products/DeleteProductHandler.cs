using BugStore.Data;
using BugStore.Requests.Products;
using BugStore.Responses.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Products
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, DeleteProductResponse?>
    {
        private readonly AppDbContext _dbContext;

        public DeleteProductHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DeleteProductResponse?> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product is null)
                return null;

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteProductResponse(product.Id, "Produto removido com sucesso.");
        }
    }
}
