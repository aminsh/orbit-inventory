using Nest;
using orbit_inventory_core.read;
using orbit_inventory_dto;

namespace orbit_inventory_read;

public class ProductRequestResolver : IReadPageableRequestResolver<ProductView, ProductFindRequest> {
    public ISearchRequest<ProductView> Resolve(ProductFindRequest findRequest)
    {
        var request = new SearchRequest<ProductView>();
        
        findRequest.ResolveBasedProperties(request);

        var must = new List<QueryContainer>();

        if (!string.IsNullOrEmpty(findRequest.Name))
            must.Add(
                new TermQuery
                {
                    Field = Infer.Field<ProductView>(f => f.Name),
                    Value = findRequest.Name,
                    CaseInsensitive = true
                }
            );

        if (!string.IsNullOrEmpty(findRequest.Upc))
            must.Add(
                new WildcardQuery
                {
                    Field = Infer.Field<ProductView>(f => f.Upc),
                    Value = findRequest.Upc,
                    CaseInsensitive = true
                }
            );

        if (findRequest.CreatedById != null)
            must.Add(
                new MatchQuery
                {
                    Field = Infer.Field<ProductView>(f => f.CreatedBy.Id),
                    Query = findRequest.CreatedById.ToString()
                }
            );

        if (must.Count > 0)
            request.Query = new BoolQuery { Must = must };

        return request;
    }
}