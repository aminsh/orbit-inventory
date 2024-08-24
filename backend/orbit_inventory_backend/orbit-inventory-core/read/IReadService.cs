using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace orbit_inventory_core.read;

public interface IReadService
{
    Task<TView> FindById<TView>(int id)
        where TView : class, IView;

    Task<IReadPageableResponse<TView>> Find<TView, TRequest>(TRequest request)
        where TRequest : class, IReadPageableRequest
        where TView : class, IView;

    Task DeleteById<TView>(int id);

    Task Create<TView>(TView view)
        where TView : class, IView;

    Task Update<TView>(int id, object updatedView)
        where TView : class, IView;
}

public class ReadService(ISearchClient searchClient, IServiceProvider provider) : IReadService
{
    public async Task<TView> FindById<TView>(int id) where TView : class, IView
    {
        var response = await searchClient.GetAsync<TView>(
            id,
            selector => selector
                .Index(ReadHelper.GetIndexNameOf<TView>())
        );

        return response.Source;
    }

    public async Task<IReadPageableResponse<TView>> Find<TView, TRequest>(TRequest request)
        where TRequest : class, IReadPageableRequest
        where TView : class, IView
    {
        var resolver = provider.GetService<IReadPageableRequestResolver<TView, TRequest>>();

        if (resolver == null)
            throw new NullReferenceException();

        var searchRequest = resolver.Resolve(request);

        var response = await searchClient.SearchAsync<TView>(searchRequest);
        return response.Resolve();
    }

    public Task DeleteById<TView>(int id)
    {
        return searchClient.DeleteAsync<TView>(new DeleteRequest(ReadHelper.GetIndexNameOf<TView>(), id));
    }

    public async Task Create<TView>(TView view)
        where TView : class, IView
    {
        await searchClient.IndexAsync(view, o => o.Index(ReadHelper.GetIndexNameOf<TView>()));
    }

    public Task Update<TView>(int id, object updatedView) 
        where TView : class, IView
    {
        return searchClient.UpdateAsync(new UpdateRequest<TView, object>(
            ReadHelper.GetIndexNameOf<TView>(), id)
        {
            Doc = updatedView
        });
    }
}