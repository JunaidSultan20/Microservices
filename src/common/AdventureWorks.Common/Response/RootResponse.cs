namespace AdventureWorks.Common.Response;

/// <summary>
/// Represents a response that contains a list of links.
/// </summary>
public class RootResponse : ApiResponse<IReadOnlyList<Links>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RootResponse"/> class with default values.
    /// </summary>
    public RootResponse()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RootResponse"/> class with the specified list of links.
    /// </summary>
    /// <param name="links">The list of links to include in the response.</param>
    /// <remarks>
    /// This constructor sets the status code to <see cref="HttpStatusCode.OK"/> and the message to "Links generated successfully".
    /// </remarks>
    public RootResponse(IReadOnlyList<Links> links) : base(HttpStatusCode.OK, message: "Links generated successfully", result: links)
    {
    }
}