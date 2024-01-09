namespace AdventureWorks.Sales.Customers.PostCustomer.Response;

public class PostCustomerResponseExample : IExamplesProvider<PostCustomerResponse>
{
    public PostCustomerResponse GetExamples()
    {
        CustomerDto customer = new CustomerDto(id: 1,
                                               personId: 150,
                                               storeId: 10,
                                               territoryId: 4,
                                               accountNumber: "AW10068267B",
                                               modifiedDate: null);

        return new PostCustomerResponse(customer);
    }
}