using Elastic.Clients.Elasticsearch;
using Nest;

namespace orbit_inventory_core.read;

public interface IReadPageableResponse<TView> where TView : IView
{
    long Count { get; set; }
    TView[] Data { get; set; }
}

public class ReadPageableResponse<TView> : IReadPageableResponse<TView> where TView : IView
{
    public long Count { get; set; }
    public TView[] Data { get; set; }
}

public static class ReadResponseResolver
{
    public static ReadPageableResponse<TView> Resolve<TView>(this ISearchResponse<TView> searchResponse)
        where TView : class, IView
    {
        return new ReadPageableResponse<TView>
        {
            Count = searchResponse.Total,
            Data = searchResponse.Hits.Select(h => h.Source).ToArray()!
        };
    }
}