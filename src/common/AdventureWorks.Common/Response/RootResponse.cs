namespace AdventureWorks.Common.Response;

public class RootResponse : ApiResponse<IReadOnlyList<Links>>
{
    public RootResponse()
    {
    }

    public RootResponse(IReadOnlyList<Links> links) : base(HttpStatusCode.OK,
                                                           message: "Links generated successfully",
                                                           links)
    {
    }
}