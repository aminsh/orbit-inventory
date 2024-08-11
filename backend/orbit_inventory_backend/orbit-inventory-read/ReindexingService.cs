using Nest;
using orbit_inventory_core.Domain;
using orbit_inventory_core.read;
using orbit_inventory_data;

namespace orbit_inventory_read;

public class ReindexingService(ElasticClient client, OrbitDbContext dbContext)
{
    public async Task Reindex<TView, TEntity>(Func<TEntity, TView> assembler)
        where TEntity : class, IEntity
        where TView : class
    {
        var indexName = ReadHelper.GetIndexNameOf<TView>();
        
        var isExistsIndex = await client.Indices.ExistsAsync(indexName);
        
        if (!isExistsIndex.Exists)
            await client.Indices.CreateAsync(indexName, c =>
                c.Map(m => m.AutoMap<TView>()));

        await client.DeleteByQueryAsync<TView>(del =>
            del
                .Index(indexName)
                .Query(q => q.QueryString(qs => qs.Query("*"))));

        var entities = dbContext.Set<TEntity>().ToList();
        await client.IndexManyAsync(
            entities.Select(assembler),
            indexName);
    }
}