using BugStore.Data;
using BugStore.Requests.Orders;
using BugStore.Responses.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Orders
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdRequest, GetOrderByIdResponse?>
    {
        private readonly AppDbContext _dbContext;

        public GetOrderByIdHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetOrderByIdResponse?> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
        {
            var order = await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.Lines)
                    .ThenInclude(line => line.Product)
                .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (order is null)
                return null;

            var orderList = new GetOrderByIdResponse(
               order.Id,
               order.CustomerId,
               order.CreatedAt,
               order.UpdatedAt,
               order.Lines.Select(l => new GetOrderLineResponse(
                   l.ProductId,
                   l.Product.Title,
                   l.Quantity,
                   l.Total
               )).ToList());
            return orderList;
        }
    }
}
