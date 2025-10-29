using BugStore.Responses.Customers;
using MediatR;

namespace BugStore.Requests.Customers;

public record CreateCustomerRequest(string Name, string Email, string Phone, DateTime BirthDate) : IRequest<CreateCustomerResponse>;