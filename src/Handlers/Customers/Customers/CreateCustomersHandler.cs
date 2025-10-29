using BugStore.Data;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using MediatR;

namespace BugStore.Handlers.Customers;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
{
    private readonly AppDbContext _appDbContext;

    public CreateCustomerHandler(AppDbContext dbContext)
    {
        _appDbContext = dbContext;
    }

    public async Task<CreateCustomerResponse> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            BirthDate = request.BirthDate
        };

        _appDbContext.Customers.Add(customer);
        await _appDbContext.SaveChangesAsync(cancellationToken); 

        return new CreateCustomerResponse(customer.Id);
    }
}