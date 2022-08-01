namespace Sales.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<BaseResponse<CustomerDto>>
{
    public int Id { get; set; }
    public UpdateCustomerDto Customer { get; set; }

    public UpdateCustomerCommand(int id, UpdateCustomerDto customer) => (Id, Customer) = (id, customer);
}