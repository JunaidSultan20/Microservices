namespace AdventureWorks.Sales.Customers.Features.DeleteCustomer.Request;

public class DeleteCustomerRequestExample : IExamplesProvider<DeleteCustomerRequest>
{
    public DeleteCustomerRequest GetExamples()
    {
        return new DeleteCustomerRequest(id: 1);
    }
}