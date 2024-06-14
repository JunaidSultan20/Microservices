using AdventureWorks.Middlewares.Exceptions;

namespace AdventureWorks.Middlewares.Test.Exceptions;

public class ExceptionMiddlewareTestData
{
    protected Mock<RequestDelegate> _mockRequestDelegate;
    private readonly Mock<ILogger<ExceptionMiddleware>> _mockLogger;
    private Exception exception;

    protected ExceptionMiddlewareTestData()
    {
        _mockRequestDelegate = new Mock<RequestDelegate>();
        _mockLogger = new Mock<ILogger<ExceptionMiddleware>>();
        exception = new Exception("Test Exception");
    }

    protected ExceptionMiddlewareTestData SetupMockMiddleware()
    {
        return this;
    }

    protected ExceptionMiddlewareTestData SetupMockMiddlewareWhenExceptionThrown()
    {
        _mockRequestDelegate.Setup(next => next(It.IsAny<HttpContext>())).Throws(exception);
        return this;
    }

    protected HttpContext CreateContext()
    {
        return new DefaultHttpContext();
    }

    public ExceptionMiddleware Build()
    {
        return new ExceptionMiddleware(_mockRequestDelegate.Object, _mockLogger.Object);
    }
}