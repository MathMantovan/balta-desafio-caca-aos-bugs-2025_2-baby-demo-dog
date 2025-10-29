using BugStore.Data;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Customers;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerRequest, DeleteCustomerResponse>
{
    private readonly AppDbContext _appDbContext;

    public DeleteCustomerHandler(AppDbContext dbContext)
    {
        _appDbContext = dbContext;
    }

    public async Task<DeleteCustomerResponse> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _appDbContext.Customers
            .AsNoTracking()
            .Where(x => x.Id == request.id)
            .FirstOrDefaultAsync(cancellationToken);

        var name = customer.Name;

        _appDbContext.Remove(customer);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        
        return new DeleteCustomerResponse(request.id, name);
    }
}