namespace AdventureWorks.Common.Helpers;

/// <summary>
/// Provides a set of helper methods for media type validation and internal property retrieval.
/// </summary>
public static class HelperMethods
{
    /// <summary>
    /// Checks if the provided media type is valid.
    /// </summary>
    /// <typeparam name="T">The type for the response value (not used directly in this method).</typeparam>
    /// <param name="mediaType">The media type to check.</param>
    /// <param name="parsedMediaType">The parsed media type if valid.</param>
    /// <param name="responseValue">The result of the check, containing an error message if the media type is invalid.</param>
    /// <returns>True if the media type is valid, otherwise false.</returns>
    public static bool CheckIfMediaTypeIsValid<T>(string? mediaType,
                                                  out MediaTypeHeaderValue? parsedMediaType,
                                                  out ApiResult? responseValue)
    {
        bool isValid = MediaTypeHeaderValue.TryParse(input: mediaType, parsedValue: out parsedMediaType);
        responseValue = null;

        if (isValid) 
            return true;

        responseValue = new ApiResult(statusCode: HttpStatusCode.UnsupportedMediaType, message: Messages.InvalidMediaType);
        return false;
    }

    /// <summary>
    /// Checks if the provided media type is valid and returns a paged response if invalid.
    /// </summary>
    /// <typeparam name="T">The type for the paged API response.</typeparam>
    /// <param name="mediaType">The media type to check.</param>
    /// <param name="parsedMediaType">The parsed media type if valid.</param>
    /// <param name="responseValue">The paged API response, containing an error message if the media type is invalid.</param>
    /// <returns>True if the media type is valid, otherwise false.</returns>
    public static bool CheckIfMediaTypeIsValid<T>(string? mediaType,
                                                  out MediaTypeHeaderValue? parsedMediaType,
                                                  out PagedApiResponse<T>? responseValue)
    {
        bool isValid = MediaTypeHeaderValue.TryParse(input: mediaType, parsedValue: out parsedMediaType);
        responseValue = null;

        if (isValid) 
            return true;

        responseValue = new PagedApiResponse<T>(statusCode: HttpStatusCode.UnsupportedMediaType, message: Messages.InvalidMediaType);
        return false;
    }

    /// <summary>
    /// Retrieves the value of an internal (non-public) property from the specified object.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property to retrieve.</typeparam>
    /// <param name="obj">The object containing the property.</param>
    /// <param name="propertyName">The name of the property to retrieve.</param>
    /// <returns>The value of the property as <typeparamref name="TProperty"/>.</returns>
    public static TProperty GetInternalProperty<TProperty>(object obj, string propertyName)
    {
        PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName, 
                                                              System.Reflection.BindingFlags.NonPublic | 
                                                              System.Reflection.BindingFlags.Instance);
        return (TProperty)propertyInfo.GetValue(obj);
    }
}