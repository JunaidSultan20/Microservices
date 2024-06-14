using System.Text;
using MongoDB.Bson;

namespace AdventureWorks.Middlewares.Test.Logging;

public class RequestLoggingMiddlewareTest : RequestLoggingMiddlewareTestData
{
    [Fact]
    public async Task LogRequestAndInvokeNextMiddleware()
    {
        // Arrange
        var sut = SetupMockClient().SetupMockOptions().Build();

        var context = CreateContext();
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes("{\"key\":\"value\"}"));
        context.Request.ContentType = Constants.ContentTypeJson;
        context.Request.Method = "POST";
        context.Request.Scheme = "https";
        context.Request.Host = new HostString("localhost");
        context.Request.Path = "/test";
        context.Request.QueryString = new QueryString("?query=string");
        context.Request.Headers["Accept"] = Constants.ContentTypeJson;
        context.Connection.RemoteIpAddress = IPAddress.Loopback;

        var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        // Act
        await sut.InvokeAsync(context);

        // Assert
        _mockNext.Verify(x => x(context), Times.Once);
        _mockCollection
               .Verify(x => 
                                x.InsertOneAsync(It.Is<BsonDocument>(doc => 
                                doc["scheme"] == context.Request.Scheme && 
                                doc["host"] == context.Request.Host.ToString() && 
                                doc["path"] == context.Request.Path.ToString() &&
                                doc["method"] == context.Request.Method &&
                                doc["query"] == context.Request.QueryString.ToString() && 
                                doc["contentType"] == context.Request.ContentType &&
                                doc["remoteIpAddress"] == context.Connection.RemoteIpAddress.ToString() &&
                                doc["body"] == "{\"key\":\"value\"}"), 
                         null, default), Times.Once);
    }
}