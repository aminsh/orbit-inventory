using Nest;
using orbit_inventory_core.read;
using orbit_inventory_data;
using orbit_inventory_domain.entity;
using orbit_inventory_dto;
using orbit_inventory_read.assembler;

namespace orbit_inventory_read;

public class ProductViewConfiguration(ElasticClient client, OrbitDbContext dbContext)
{
    public async Task Configure()
    {
        var indexName = ReadHelper.GetIndexNameOf<ProductView>();
        var result = await client.Indices.CreateAsync(indexName, c =>
            c.Map(m => m.AutoMap<ProductView>()));

        await client.DeleteByQueryAsync<ProductView>(del => 
            del
                .Index(indexName)
                .Query(q => q.QueryString(qs => qs.Query("*"))));

        var products = dbContext.Set<Product>().ToList();
        await client.IndexManyAsync(
            products.Select(ProductAssembler.Assemble), 
            indexName);
    }
}