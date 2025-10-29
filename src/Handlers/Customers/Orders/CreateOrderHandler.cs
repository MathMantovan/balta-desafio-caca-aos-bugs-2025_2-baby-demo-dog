using BugStore.Data;
using BugStore.Models;
using BugStore.Requests.Orders;
using BugStore.Responses.Orders;
using MediatR;

namespace BugStore.Handlers.Orders
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
    {
        private readonly AppDbContext _dbContext;

        public CreateOrderHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateOrderResponse> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Lines = request.Lines.Select(line => new OrderLine
                {
                    Id = Guid.NewGuid(),
                    ProductId = line.ProductId,
                    Quantity = line.Quantity,
                    Total = line.Total
                }).ToList()
            };

            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderResponse(order.Id);
        }
    }
}
