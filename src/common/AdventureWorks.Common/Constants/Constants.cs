namespace AdventureWorks.Common.Constants;

public static class Constants
{
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 10;

    public const string ContentTypeJson = "application/json";
    public const string ContentTypeXml = "application/xml";
    public const string ContentTypeTextPlain = "text/plain";
    public const string ContentTypeTextJson = "text/json";
    public const string ContentTypeJsonHateoas = "application/vnd.api.hateoas+json";
    public const string ContentTypeXmlHateoas = "application/vnd.api.hateoas+xml";

    public const string LoginProviderName = "AdventureWorks.Identity";
    public const string TokenName = "RefreshToken";
    public const string BearerToken = "bearerToken";

    public const string ForwardedFor = "X-Forwarded-For";
    public const string XPaginationKey = "X-Pagination";
    public const string RemoteIpAddress = "RemoteIpAddress";

    public const string SelfRel = "self";
    public const string CustomerRel = "customer";
    public const string StoreRel = "store";
    public const string GetMethod = "GET";
    public const string PostMethod = "POST";
    public const string PutMethod = "PUT";
    public const string DeleteMethod = "DELETE";

    public const string ApiValue = "/api";
    public const string VndApiHateoas = "vnd.api.hateoas";

    public const string DefaultConnection = "DefaultConnection";

    public const string SalesQueue = "salesQueue";
    public const string ProductionQueue = "productionQueue";
    public const string IdentityQueue = "identityQueue";
}