namespace AdventureWorks.Sales.Customers.Features.GetCustomerById.Response;

/// <summary>
/// Provides an example of <see cref="GetCustomerByIdShapedResponse"/>.
/// Implements <see cref="IExamplesProvider{T}"/> for generating example responses.
/// </summary>
public class GetCustomerByIdShapedResponseExample : IExamplesProvider<GetCustomerByIdShapedResponse>
{
    /// <summary>
    /// Gets an example of <see cref="GetCustomerByIdShapedResponse"/>.
    /// </summary>
    /// <returns>A <see cref="GetCustomerByIdShapedResponse"/> instance containing an example customer data.</returns>
    public GetCustomerByIdShapedResponse GetExamples()
    {
        CustomerDto customer = new CustomerDto(customerId: 1,
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

        return new GetCustomerByIdShapedResponse("Record retrieved successfully",
                                                 customer.ShapeData("id, accountNumber"));
    }
}