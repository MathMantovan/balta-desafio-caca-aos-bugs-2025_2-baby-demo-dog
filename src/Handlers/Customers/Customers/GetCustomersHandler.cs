using BugStore.Data;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Customers;

public class GetCustomersHandler : IRequestHandler<GetCustomerRequest, IEnumerable<GetCustomerResponse>>
{
    private readonly AppDbContext _appDbContext;

    public GetCustomersHandler(AppDbContext dbContext)
    {
        _appDbContext = dbContext;
    }

    public async Task<IEnumerable<GetCustomerResponse>> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
    {
        var customers = await _appDbContext.Customers
            .AsNoTracking()
            .Select(p => new GetCustomerResponse(p.Id, p.Name, p.Email, p.Phone, p.BirthDate))
            .ToListAsync(cancellationToken);

        return customers;
    }
}