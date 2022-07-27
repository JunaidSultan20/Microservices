using Sales.Application.DTOs.Customer;

namespace Sales.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerByIdCommand : IRequest<BaseResponse<CustomerDto>>
{
    public int Id { get; set; }
    public UpdateCustomerDto Customer { get; set; }

    public UpdateCustomerByIdCommand(int id, UpdateCustomerDto customer) => (Id, Customer) = (id, customer);
}