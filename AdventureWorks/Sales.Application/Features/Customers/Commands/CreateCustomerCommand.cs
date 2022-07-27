namespace Sales.Application.Features.Customers.Commands;

public class CreateCustomerCommand : IRequest<BaseResponse<CustomerDto>>
{
    public CreateCustomerDto Customer { get; }

    public CreateCustomerCommand(CreateCustomerDto customer) => Customer = customer;
}