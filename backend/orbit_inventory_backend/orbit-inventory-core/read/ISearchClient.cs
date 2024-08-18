using Nest;

namespace orbit_inventory_core.read;

public interface ISearchClient
{
    Task<GetResponse<TDocument>> GetAsync<TDocument>(
        DocumentPath<TDocument> id,
        Func<GetDescriptor<TDocument>, IGetRequest>? selector = null
    )
        where TDocument : class;

    Task<ISearchResponse<TDocument>> SearchAsync<TDocument>(ISearchRequest request)
        where TDocument : class;

    Task<ExistsResponse> ExistsIndexAsync(Indices index,
        Func<IndexExistsDescriptor, IIndexExistsRequest>? selector = null);

    Task<DeleteIndexResponse> DeleteIndexAsync(Indices index,
        Func<DeleteIndexDescriptor, IDeleteIndexRequest>? selector = null);

    Task<CreateIndexResponse> CreateIndexAsync(IndexName index,
        Func<CreateIndexDescriptor, ICreateIndexRequest>? selector = null);

    Task<BulkResponse> IndexManyAsync<T>(IEnumerable<T> objects, IndexName? index = null)
        where T : class;

    Task<IndexResponse> IndexAsync<TDocument>(
        TDocument document,
        Func<IndexDescriptor<TDocument>, IIndexRequest<TDocument>> selector)
        where TDocument : class;

    Task<DeleteResponse> DeleteAsync<TDocument>(IDeleteRequest request);

    Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(
        IUpdateRequest<TDocument, TPartialDocument> request)
        where TDocument : class
        where TPartialDocument : class;
}

public class SearchClient(ElasticClient client) : ISearchClient
{
    public Task<GetResponse<TDocument>> GetAsync<TDocument>(
        DocumentPath<TDocument> id,
        Func<GetDescriptor<TDocument>, IGetRequest>? selector = null
    )
        where TDocument : class
    {
        return client.GetAsync<TDocument>(id, selector);
    }

    public Task<ISearchResponse<TDocument>> SearchAsync<TDocument>(ISearchRequest request)
        where TDocument : class
    {
        return client.SearchAsync<TDocument>(request);
    }

    public Task<ExistsResponse> ExistsIndexAsync(Indices index,
        Func<IndexExistsDescriptor, IIndexExistsRequest>? selector = null)
    {
        return client.Indices.ExistsAsync(index, selector);
    }

    public Task<DeleteIndexResponse> DeleteIndexAsync(Indices index,
        Func<DeleteIndexDescriptor, IDeleteIndexRequest>? selector = null)
    {
        return client.Indices.DeleteAsync(index, selector);
    }

    public Task<CreateIndexResponse> CreateIndexAsync(IndexName index, Func<CreateIndexDescriptor, ICreateIndexRequest>? selector = null)
    {
        return client.Indices.CreateAsync(index, selector);
    }

    public Task<BulkResponse> IndexManyAsync<T>(IEnumerable<T> objects, IndexName? index = null)
        where T : class
    {
        return client.IndexManyAsync(objects, index);
    }

    public Task<IndexResponse> IndexAsync<TDocument>(
        TDocument document,
        Func<IndexDescriptor<TDocument>, IIndexRequest<TDocument>> selector)
        where TDocument : class
    {
        return client.IndexAsync(document, selector);
    }

    public Task<DeleteResponse> DeleteAsync<TDocument>(IDeleteRequest request)
    {
        return client.DeleteAsync(request);
    }

    public Task<UpdateResponse<TDocument>> UpdateAsync<TDocument, TPartialDocument>(
        IUpdateRequest<TDocument, TPartialDocument> request)
        where TDocument : class
        where TPartialDocument : class
    {
        return client.UpdateAsync(request);
    }
}