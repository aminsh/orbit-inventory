using orbit_inventory_core.Domain;
using orbit_inventory_core.read;
using orbit_inventory_data;

namespace orbit_inventory_read;

public class ReIndexingService<TEntity, TView>(
    ISearchClient client,
    OrbitDbContext dbContext,
    IViewMapping<TView> viewMapping,
    IViewAssembler<TEntity, TView> viewAssembler)
    where TEntity : class, IEntity
    where TView : class, IView
{
    public async Task ReIndex()
    {
        var indexName = ReadHelper.GetIndexNameOf<TView>();

        var isExistsIndex = await client.ExistsIndexAsync(indexName);

        if (isExistsIndex.Exists)
            await client.DeleteIndexAsync(indexName);

        await client.CreateIndexAsync(indexName, c => c
            .Map<TView>(viewMapping.Map)
        );

        var entities = dbContext.Set<TEntity>().ToList();
        var views = await Task.WhenAll(entities.Select(viewAssembler.Assemble));

        await client.IndexManyAsync(views, indexName);
    }
}