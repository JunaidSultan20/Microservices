namespace AdventureWorks.Sales.Customers.GetCustomerById.Response;

public class GetCustomerByIdShapedResponseExample : IExamplesProvider<GetCustomerByIdShapedResponse>
{
    public GetCustomerByIdShapedResponse GetExamples()
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

        return new GetCustomerByIdShapedResponse(message: Messages.RecordsRetrievedSuccessfully,
                                                 result: customer.ShapeData(fields: "id, accountNumber"));
    }
}