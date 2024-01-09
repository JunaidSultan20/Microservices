namespace AdventureWorks.Common.Response;

public class RootResponse : ApiResponse<IReadOnlyList<Links>>
{
    public RootResponse(IReadOnlyList<Links> links) : base(HttpStatusCode.OK,
                                                           message: "Links generated successfully",
                                                           links)
    {
    }
}