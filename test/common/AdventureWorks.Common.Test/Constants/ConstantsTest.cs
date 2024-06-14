namespace AdventureWorks.Common.Test.Constants;

public class ConstantsTest
{
    [Fact]
    public void Constants_ShouldHaveCorrectValues()
    {
        // Assert
        Common.Constants.Constants.DefaultPageNumber.Should().Be(1);
        Common.Constants.Constants.DefaultPageNumber.Should().BeOfType(typeof(int));

        Common.Constants.Constants.DefaultPageSize.Should().Be(10);
        Common.Constants.Constants.DefaultPageSize.Should().BeOfType(typeof(int));

        Common.Constants.Constants.ContentTypeJson.Should().Be("application/json");
        Common.Constants.Constants.ContentTypeJson.Should().BeOfType(typeof(string));

        Common.Constants.Constants.ContentTypeXml.Should().Be("application/xml");
        Common.Constants.Constants.ContentTypeXml.Should().BeOfType(typeof(string));

        Common.Constants.Constants.ContentTypeTextPlain.Should().Be("text/plain");
        Common.Constants.Constants.ContentTypeTextPlain.Should().BeOfType(typeof(string));

        Common.Constants.Constants.ContentTypeTextJson.Should().Be("text/json");
        Common.Constants.Constants.ContentTypeTextJson.Should().BeOfType(typeof(string));

        Common.Constants.Constants.ContentTypeJsonHateoas.Should().Be("application/vnd.api.hateoas+json");
        Common.Constants.Constants.ContentTypeJsonHateoas.Should().BeOfType(typeof(string));

        Common.Constants.Constants.ContentTypeXmlHateoas.Should().Be("application/vnd.api.hateoas+xml");
        Common.Constants.Constants.ContentTypeXmlHateoas.Should().BeOfType(typeof(string));

        Common.Constants.Constants.LoginProviderName.Should().Be("AdventureWorks.Identity");
        Common.Constants.Constants.LoginProviderName.Should().BeOfType(typeof(string));

        Common.Constants.Constants.TokenName.Should().Be("RefreshToken");
        Common.Constants.Constants.TokenName.Should().BeOfType(typeof(string));

        Common.Constants.Constants.BearerToken.Should().Be("bearerToken");
        Common.Constants.Constants.BearerToken.Should().BeOfType(typeof(string));

        Common.Constants.Constants.ForwardedFor.Should().Be("X-Forwarded-For");
        Common.Constants.Constants.ForwardedFor.Should().BeOfType(typeof(string));

        Common.Constants.Constants.XPaginationKey.Should().Be("X-Pagination");
        Common.Constants.Constants.XPaginationKey.Should().BeOfType(typeof(string));

        Common.Constants.Constants.RemoteIpAddress.Should().Be("RemoteIpAddress");
        Common.Constants.Constants.RemoteIpAddress.Should().BeOfType(typeof(string));

        Common.Constants.Constants.SelfRel.Should().Be("self");
        Common.Constants.Constants.SelfRel.Should().BeOfType(typeof(string));

        Common.Constants.Constants.CustomerRel.Should().Be("customer");
        Common.Constants.Constants.CustomerRel.Should().BeOfType(typeof(string));

        Common.Constants.Constants.StoreRel.Should().Be("store");
        Common.Constants.Constants.StoreRel.Should().BeOfType(typeof(string));

        Common.Constants.Constants.GetMethod.Should().Be("GET");
        Common.Constants.Constants.GetMethod.Should().BeOfType(typeof(string));

        Common.Constants.Constants.PostMethod.Should().Be("POST");
        Common.Constants.Constants.PostMethod.Should().BeOfType(typeof(string));

        Common.Constants.Constants.PutMethod.Should().Be("PUT");
        Common.Constants.Constants.PutMethod.Should().BeOfType(typeof(string));

        Common.Constants.Constants.DeleteMethod.Should().Be("DELETE");
        Common.Constants.Constants.DeleteMethod.Should().BeOfType(typeof(string));

        Common.Constants.Constants.ApiValue.Should().Be("/api");
        Common.Constants.Constants.ApiValue.Should().BeOfType(typeof(string));

        Common.Constants.Constants.VndApiHateoas.Should().Be("vnd.api.hateoas");
        Common.Constants.Constants.VndApiHateoas.Should().BeOfType(typeof(string));

        Common.Constants.Constants.DefaultConnection.Should().Be("DefaultConnection");
        Common.Constants.Constants.DefaultConnection.Should().BeOfType(typeof(string));

        Common.Constants.Constants.SalesQueue.Should().Be("salesQueue");
        Common.Constants.Constants.SalesQueue.Should().BeOfType(typeof(string));

        Common.Constants.Constants.ProductionQueue.Should().Be("productionQueue");
        Common.Constants.Constants.ProductionQueue.Should().BeOfType(typeof(string));

        Common.Constants.Constants.IdentityQueue.Should().Be("identityQueue");
        Common.Constants.Constants.IdentityQueue.Should().BeOfType(typeof(string));
    }
}