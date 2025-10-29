using BugStore.Data;
using BugStore.Models;
using BugStore.Requests.Customers;
using BugStore.Responses.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Handlers.Customers;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResponse>
{
    private readonly AppDbContext _appDbContext;

    public UpdateCustomerHandler(AppDbContext dbContext)
    {
        _appDbContext = dbContext;
    }

    public async Task<UpdateCustomerResponse?> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _appDbContext.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);

        if (customer == null)
            return null;

        customer.Name = request.DataRequest.Name;
        customer.Email = request.DataRequest.Email;
        customer.Phone = request.DataRequest.Phone;
        customer.BirthDate = request.DataRequest.BirthDate;

        _appDbContext.Customers.Update(customer);
        await _appDbContext.SaveChangesAsync(cancellationToken); 

        return new UpdateCustomerResponse(request.Id);
    }
}