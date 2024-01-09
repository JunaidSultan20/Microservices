namespace AdventureWorks.Common.Helpers;

public static class HelperMethods
{
    public static bool CheckIfMediaTypeIsValid<T>(string? mediaType,
                                                  out MediaTypeHeaderValue? parsedMediaType,
                                                  out ApiResult? responseValue)
    {
        bool isValid = MediaTypeHeaderValue.TryParse(input: mediaType, parsedValue: out parsedMediaType);
        responseValue = null;
        if (isValid) return true;
        responseValue = new ApiResult(statusCode: HttpStatusCode.UnsupportedMediaType, message: Messages.InvalidMediaType);
        return false;
    }

    public static bool CheckIfMediaTypeIsValid<T>(string? mediaType,
                                                  out MediaTypeHeaderValue? parsedMediaType,
                                                  out PagedApiResponse<T>? responseValue)
    {
        bool isValid = MediaTypeHeaderValue.TryParse(input: mediaType, parsedValue: out parsedMediaType);
        responseValue = null;
        if (isValid) return true;
        responseValue = new PagedApiResponse<T>(statusCode: HttpStatusCode.UnsupportedMediaType, message: Messages.InvalidMediaType);
        return false;
    }
}