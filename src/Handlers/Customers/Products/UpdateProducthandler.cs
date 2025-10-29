using BugStore.Commands.Products;
using BugStore.Data;
using BugStore.Responses.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Products
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse?>
    {
        private readonly AppDbContext _dbContext;

        public UpdateProductHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UpdateProductResponse?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (product is null)
                return null;

            product.Title = request.Data.Title;
            product.Description = request.Data.Description;
            product.Slug = request.Data.Slug;
            product.Price = request.Data.Price;

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateProductResponse(product.Id);
        }
    }
}
