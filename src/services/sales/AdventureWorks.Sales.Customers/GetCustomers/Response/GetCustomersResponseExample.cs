namespace AdventureWorks.Sales.Customers.GetCustomers.Response;

public class GetCustomersResponseExample : IExamplesProvider<GetCustomersResponse>
{
    public GetCustomersResponse GetExamples()
    {
        List<CustomerDto> customerList = new List<CustomerDto>
        {
            new CustomerDto(id: 1, personId: 150, storeId: 10, territoryId: 4, accountNumber: "AW10068267B", modifiedDate: null, links: new List<Links>
            {
                new Links(href: "https://www.xyz.com/api/Customer/1", rel: Constants.GetMethod, method: Constants.SelfRel),
                new Links(href: "https://www.xyz.com/api/Customer/1", rel: Constants.DeleteMethod, method: "delete_customer"),
                new Links(href: "https://www.xyz.com/api/Customer/1", rel: Constants.PutMethod, method: "update_customer")
            }),

            new CustomerDto(id: 2, personId: 148, storeId: 50, territoryId: 4, accountNumber: "AW24871696Z", modifiedDate: null, links: new List<Links>
            {
                new Links(href: "https://www.xyz.com/api/Customer/2", rel: Constants.GetMethod, method: Constants.SelfRel),
                new Links(href: "https://www.xyz.com/api/Customer/2", rel: Constants.DeleteMethod, method: "delete_customer"),
                new Links(href: "https://www.xyz.com/api/Customer/2", rel: Constants.PutMethod, method: "update_customer")
            }),

            new CustomerDto(id: 3, personId: 20, storeId: 10, territoryId: 3, accountNumber: "AW26769234A", modifiedDate: null, links: new List<Links>
            {
                new Links(href: "https://www.xyz.com/api/Customer/3", rel: Constants.GetMethod, method: Constants.SelfRel),
                new Links(href: "https://www.xyz.com/api/Customer/3", rel: Constants.DeleteMethod, method: "delete_customer"),
                new Links(href: "https://www.xyz.com/api/Customer/3", rel: Constants.PutMethod, method: "update_customer")
            }),

            new CustomerDto(id: 4, personId: 150, storeId: 10, territoryId: 4, accountNumber: "AW96438732A", modifiedDate: null, links: new List<Links>
            {
                new Links(href: "https://www.xyz.com/api/Customer/4", rel: Constants.GetMethod, method: Constants.SelfRel),
                new Links(href: "https://www.xyz.com/api/Customer/4", rel: Constants.DeleteMethod, method: "delete_customer"),
                new Links(href: "https://www.xyz.com/api/Customer/4", rel: Constants.PutMethod, method: "update_customer")
            }),

            new CustomerDto(id: 5, personId: 20, storeId: 50, territoryId: 1, accountNumber: "AW44680085Z", modifiedDate: null, links: new List<Links>
            {
                new Links(href: "https://www.xyz.com/api/Customer/5", rel: Constants.GetMethod, method: Constants.SelfRel),
                new Links(href: "https://www.xyz.com/api/Customer/5", rel: Constants.DeleteMethod, method: "delete_customer"),
                new Links(href: "https://www.xyz.com/api/Customer/5", rel: Constants.PutMethod, method: "update_customer")
            })
        };

        var paginationData = new PaginationData(totalRecords: 100,
                                                pageNumber: 1,
                                                pageSize: 5,
                                                url: "https://www.xyz.com/api/customer");

        return new GetCustomersResponse(result: customerList.AsReadOnly(),
                                        pagination: paginationData);
    }
}