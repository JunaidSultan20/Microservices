namespace AdventureWorks.Sales.Customers.PutCustomer.Response;

public class BadRequestCustomerResponseExample : IExamplesProvider<BadRequestCustomerResponse>
{
    public BadRequestCustomerResponse GetExamples()
    {
        return new BadRequestCustomerResponse("Unable to update the customer");
    }
}