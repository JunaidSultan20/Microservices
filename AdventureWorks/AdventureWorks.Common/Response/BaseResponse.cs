namespace AdventureWorks.Common.Response;

/// <summary>
/// Generic BaseResponse
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class BaseResponse<TEntity>
{
    /// <summary>
    /// Returns the status code of the request
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// Message description
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Success/Fail indicator
    /// </summary>
    public bool? IsSuccessful { get; set; }

    /// <summary>
    /// TEntity of result type
    /// </summary>
    public TEntity? Result { get; set; }

    /// <summary>
    /// Links for HATEOAS
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public IReadOnlyList<Links>? Links { get; set; }

    /// <summary>
    /// Api exception details
    /// </summary>
    public ApiException? ApiException { get; set; }

    /// <summary>
    /// List of errors
    /// </summary>
    public List<string>? Errors { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public BaseResponse() { }

    /// <summary>
    /// Constructor with status code, message and result parameters
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="message"></param>
    /// <param name="result"></param>
    //public BaseResponse(HttpStatusCode statusCode, string? message, TEntity? result)
    //{
    //    switch (statusCode)
    //    {
    //        case HttpStatusCode.OK:
    //            StatusCode = HttpStatusCode.OK;
    //            Message = message ?? "Records retrieved successfully.";
    //            IsSuccessful = true;
    //            Result = result;
    //            break;
    //        case HttpStatusCode.Created:
    //            StatusCode = HttpStatusCode.Created;
    //            Message = message ?? "Record added successfully.";
    //            IsSuccessful = true;
    //            Result = result;
    //            break;
    //        case HttpStatusCode.NoContent:
    //            StatusCode = HttpStatusCode.NoContent;
    //            Message = message ?? "Resource deleted successfully.";
    //            IsSuccessful = true;
    //            Result = result;
    //            break;
    //        case HttpStatusCode.BadRequest:
    //            StatusCode = HttpStatusCode.BadRequest;
    //            Message = message ?? "Invalid resource requested.";
    //            IsSuccessful = false;
    //            Result = result;
    //            break;
    //        case HttpStatusCode.Unauthorized:
    //            StatusCode = HttpStatusCode.Unauthorized;
    //            Message = message ?? "Invalid authentication request.";
    //            IsSuccessful = false;
    //            break;
    //        case HttpStatusCode.NotFound:
    //            StatusCode = HttpStatusCode.NotFound;
    //            Message = message ?? "No records found.";
    //            IsSuccessful = false;
    //            break;
    //        case HttpStatusCode.MethodNotAllowed:
    //            StatusCode = HttpStatusCode.MethodNotAllowed;
    //            Message = message ?? "Method not allowed.";
    //            IsSuccessful = false;
    //            break;
    //        case HttpStatusCode.Conflict:
    //            StatusCode = HttpStatusCode.Conflict;
    //            Message = message ?? "Request conflict.";
    //            IsSuccessful = false;
    //            break;
    //        case HttpStatusCode.UnsupportedMediaType:
    //            StatusCode = HttpStatusCode.UnsupportedMediaType;
    //            Message = message ?? "Media type not supported.";
    //            IsSuccessful = false;
    //            break;
    //        case HttpStatusCode.InternalServerError:
    //            StatusCode = HttpStatusCode.InternalServerError;
    //            Message = message ?? "Internal server error occurred.";
    //            IsSuccessful = false;
    //            break;
    //        case HttpStatusCode.NotAcceptable:
    //            StatusCode = HttpStatusCode.NotAcceptable;
    //            Message = message ?? "Request body not acceptable.";
    //            IsSuccessful = false;
    //            break;
    //        default:
    //            return;
    //    }
    //}

    public BaseResponse(HttpStatusCode statusCode, string? message)
    {
        switch (statusCode)
        {
            case HttpStatusCode.OK:
                StatusCode = HttpStatusCode.OK;
                Message = message ?? "Records retrieved successfully.";
                IsSuccessful = true;
                break;
            case HttpStatusCode.Created:
                StatusCode = HttpStatusCode.Created;
                Message = message ?? "Record added successfully.";
                IsSuccessful = true;
                break;
            case HttpStatusCode.NoContent:
                StatusCode = HttpStatusCode.NoContent;
                Message = message ?? "Resource deleted successfully.";
                IsSuccessful = true;
                break;
            case HttpStatusCode.BadRequest:
                StatusCode = HttpStatusCode.BadRequest;
                Message = message ?? "Invalid resource requested.";
                IsSuccessful = false;
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

    public BaseResponse(HttpStatusCode statusCode, string? message, TEntity? result) : this(statusCode, message)
    {
        Result = result;
    }

    public BaseResponse(HttpStatusCode statusCode, string? message, List<string>? errors) : this(statusCode, message)
    {
        Errors = errors;
    }
}