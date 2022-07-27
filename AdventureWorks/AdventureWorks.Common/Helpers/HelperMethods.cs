using AdventureWorks.Common.Response;
using Microsoft.Net.Http.Headers;

namespace AdventureWorks.Common.Helpers;

public static class HelperMethods
{
    public static bool CheckIfMediaTypeIsValid<T>(string? mediaType, out MediaTypeHeaderValue? parsedMediaType, out BaseResponse<T>? responseValue)
    {
        bool isValid = MediaTypeHeaderValue.TryParse(mediaType, out parsedMediaType);
        responseValue = null;
        if (!isValid)
        {
            responseValue = new BaseResponse<T>(HttpStatusCode.UnsupportedMediaType, "Invalid media type provided");
            return false;
        }
        return true;
    }

    public static bool CheckIfMediaTypeIsValid<T>(string? mediaType, out MediaTypeHeaderValue? parsedMediaType, out PaginationResponse<T>? responseValue)
    {
        bool isValid = MediaTypeHeaderValue.TryParse(mediaType, out parsedMediaType);
        responseValue = null;
        if (!isValid)
        {
            responseValue = new PaginationResponse<T>(HttpStatusCode.UnsupportedMediaType, "Invalid Media Type Provided");
            return false;
        }
        return true;
    }
}