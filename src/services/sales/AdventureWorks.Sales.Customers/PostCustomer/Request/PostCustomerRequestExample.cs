namespace AdventureWorks.Sales.Customers.PostCustomer.Request;

public class PostCustomerRequestExample : IExamplesProvider<PostCustomerRequest>
{
    public PostCustomerRequest GetExamples()
    {
        CreateCustomerDto customerDto = new CreateCustomerDto(personId: 150,
                                                              storeId: 10,
                                                              territoryId: 4,
                                                              accountNumber: "AW10068267B");

        return new PostCustomerRequest(customerDto);
    }
}