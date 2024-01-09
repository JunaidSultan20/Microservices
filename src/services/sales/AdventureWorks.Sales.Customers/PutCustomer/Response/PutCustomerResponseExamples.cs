namespace AdventureWorks.Sales.Customers.PutCustomer.Response;

public class PutCustomerResponseExamples : IExamplesProvider<PutCustomerResponse>
{
    public PutCustomerResponse GetExamples()
    {
        CustomerDto customer = new CustomerDto(id: 1,
                                               personId: 150,
                                               storeId: 10,
                                               territoryId: 4,
                                               accountNumber: "AW10068267B",
                                               modifiedDate: null,
                                               links: new List<Links>
                                               {
                                                   new Links(href: "https://www.xyz.com/api/Customer/1",
                                                             rel: Constants.GetMethod,
                                                             method: Constants.SelfRel),
                                                   new Links(href: "https://www.xyz.com/api/Customer/1",
                                                             rel: Constants.DeleteMethod,
                                                             method: "delete_customer"),
                                                   new Links(href: "https://www.xyz.com/api/Customer/1",
                                                             rel: Constants.PutMethod,
                                                             method: "update_customer")
                                               });

        return new PutCustomerResponse(customer);
    }
}