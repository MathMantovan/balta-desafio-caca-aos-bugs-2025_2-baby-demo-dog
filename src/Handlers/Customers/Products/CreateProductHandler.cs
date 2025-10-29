using BugStore.Data;
using BugStore.Models;
using BugStore.Requests.Products;
using BugStore.Responses.Products;
using MediatR;

namespace BugStore.Handlers.Products
{
    public class CreateProductHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>
    {
        private readonly AppDbContext _dbContext;

        public CreateProductHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Slug = request.Slug,
                Price = request.Price
            };

            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateProductResponse(
                product.Id,
                product.Title,
                product.Description,
                product.Slug,
                product.Price
            );
        }
    }
}
