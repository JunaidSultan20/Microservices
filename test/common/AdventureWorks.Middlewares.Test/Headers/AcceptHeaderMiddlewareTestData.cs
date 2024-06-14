using AdventureWorks.Middlewares.Headers;

namespace AdventureWorks.Middlewares.Test.Headers;

public class AcceptHeaderMiddlewareTestData
{
    protected readonly Mock<RequestDelegate> _mockNext;
    private readonly Mock<ILogger<AcceptHeaderMiddleware>> _mockLogger;

    protected AcceptHeaderMiddlewareTestData()
    {
        _mockNext = new Mock<RequestDelegate>();
        _mockLogger = new Mock<ILogger<AcceptHeaderMiddleware>>();
    }

    protected AcceptHeaderMiddlewareTestData SetupMockAcceptHeaderMiddleware()
    {
        return this;
    }

    protected HttpContext CreateContext()
    {
        return new DefaultHttpContext();
    }

    public AcceptHeaderMiddleware Build()
    {
        return new AcceptHeaderMiddleware(_mockNext.Object, _mockLogger.Object);
    }
}