using Nest;

namespace orbit_inventory_core.read;

public interface IReadPageableRequest
{
    int Take { get; set; }
    int Skip { get; set; }
}

public interface IReadPageableRequestResolver<TView, in TRequest> 
{
    ISearchRequest<TView> Resolve(TRequest request);
}

public static class ReadPageableRequestBaseResolver
{
    public static void ResolveBasedProperties(this IReadPageableRequest request, ISearchRequest searchRequest)
    {
        searchRequest.From = request.Skip;
        searchRequest.Size = request.Take;
    }
}