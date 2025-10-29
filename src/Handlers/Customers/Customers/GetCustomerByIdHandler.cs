using BugStore.Data;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Customers;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdRequest, GetCustomerByIdResponse>
{
    private readonly AppDbContext _appDbContext;

    public GetCustomerByIdHandler(AppDbContext dbContext)
    {
        _appDbContext = dbContext;
    }

    public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
    {
        var customer = await _appDbContext.Customers
            .AsNoTracking()
            .Where(x => x.Id == request.id)
            .Select(p => new GetCustomerByIdResponse(p.Id, p.Name, p.Email, p.Phone, p.BirthDate))
            .FirstOrDefaultAsync(cancellationToken);

        return customer;
    }
}