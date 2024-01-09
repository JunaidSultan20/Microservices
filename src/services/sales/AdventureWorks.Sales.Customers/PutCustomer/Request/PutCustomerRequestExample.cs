namespace AdventureWorks.Sales.Customers.PutCustomer.Request;

public class PutCustomerRequestExample : IExamplesProvider<PutCustomerRequest>
{
    public PutCustomerRequest GetExamples()
    {
        UpdateCustomerDto customerDto = new UpdateCustomerDto(personId: 150,
                                                              storeId: 10,
                                                              territoryId: 4,
                                                              accountNumber: "AW10068267B");

        return new PutCustomerRequest(customerDto);
    }
}