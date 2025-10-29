namespace BugStore.Responses.Customers;

public record GetCustomerByIdResponse(Guid Id, string Name, string Email, string Phone, DateTime BirthDate);