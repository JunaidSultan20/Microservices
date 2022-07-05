namespace AdventureWorks.Common.Response;

public class BaseResponse<TEntity>
{
    public HttpStatusCode StatusCode { get; set; }

    public string? Message { get; set; }

    public bool? IsSuccessful { get; set; }

    public TEntity? Result { get; set; }

    public IReadOnlyList<Links>? Links { get; set; }

    public ApiException? ApiException { get; set; }

    public List<string>? Errors { get; set; }

    public BaseResponse() { }

    public BaseResponse(HttpStatusCode statusCode, string? message, TEntity? result)
    {
        switch (statusCode)
        {
            case HttpStatusCode.OK:
                StatusCode = HttpStatusCode.OK;
                Message = message ?? "Records retrieved successfully.";
                IsSuccessful = true;
                Result = result;
                break;
            case HttpStatusCode.Created:
                StatusCode = HttpStatusCode.Created;
                Message = message ?? "Record added successfully.";
                IsSuccessful = true;
                Result = result;
                break;
            case HttpStatusCode.NoContent:
                StatusCode = HttpStatusCode.NoContent;
                Message = message ?? "Resource deleted successfully.";
                IsSuccessful = true;
                Result = result;
                break;
            case HttpStatusCode.BadRequest:
                StatusCode = HttpStatusCode.BadRequest;
                Message = message ?? "Invalid resource requested.";
                IsSuccessful = false;
                Result = result;
                break;
            case HttpStatusCode.Unauthorized:
                StatusCode = HttpStatusCode.Unauthorized;
                Message = message ?? "Invalid authentication request.";
                IsSuccessful = false;
                break;
            case HttpStatusCode.NotFound:
                StatusCode = HttpStatusCode.NotFound;
                Message = message ?? "No records found.";
                IsSuccessful = false;
                break;
            case HttpStatusCode.MethodNotAllowed:
                StatusCode = HttpStatusCode.MethodNotAllowed;
                Message = message ?? "Method not allowed.";
                IsSuccessful = false;
                break;
            case HttpStatusCode.Conflict:
                StatusCode = HttpStatusCode.Conflict;
                Message = message ?? "Request conflict.";
                IsSuccessful = false;
                break;
            case HttpStatusCode.UnsupportedMediaType:
                StatusCode = HttpStatusCode.UnsupportedMediaType;
                Message = message ?? "Media type not supported.";
                IsSuccessful = false;
                break;
            case HttpStatusCode.InternalServerError:
                StatusCode = HttpStatusCode.InternalServerError;
                Message = message ?? "Internal server error occurred.";
                IsSuccessful = false;
                break;
            case HttpStatusCode.NotAcceptable:
                StatusCode = HttpStatusCode.NotAcceptable;
                Message = message ?? "Request body not acceptable.";
                IsSuccessful = false;
                break;
            default:
                return;
        }
    }
}