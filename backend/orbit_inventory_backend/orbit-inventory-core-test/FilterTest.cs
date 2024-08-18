using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Newtonsoft.Json.Linq;

namespace orbit_inventory_core_test;

public class FilterTest
{
    [Test]
    public async Task TestFilter()
    {
        var obj = JObject.Parse(@"{
                  ""sold"": 43,
                  ""country_id"": 1
                }"
        );
        var resolver = new NumberResolver();
        var client = new ElasticsearchClient(new Uri("http://localhost:1004"));

        /*var q = new MatchQuery("")
        {
            Query = 12
        };*/

        /*.Query(new BoolQuery
                {
                    Must  = new List<Query>
                    {
                       q
                    }
                })*/
        /*foreach (var jProperty in obj.Properties())
                       {
                           s = resolver.Resolve(s, jProperty);
                       }*/
        var res = await client.SearchAsync<Sku>(r =>
            r.Index("skus-stage")
                .From(0)
                .Size(20)
                .Query(q => q
                    .Bool(b => b
                        .Must(mu => mu
                            .Match(m => m
                                .Field(f => f.Sold)
                                .Query(888)
                            )
                            .Match(m => m
                                .Field(f => f.country_id)
                                .Query(1)
                            )
                        )
                    )
                )
                .Query(new BoolQuery
                {
                    Must = new List<Query>
                    {
                        new MatchQuery(Infer.Field<Sku>(f => f.country_id)) { Query = 2 },
                        //new MatchQuery(new Field("sold")) { Query = 43 },
                    }
                })
        );
    }
}

public class NumberResolver
{
    public QueryDescriptor<Sku> Resolve(QueryDescriptor<Sku> queryDescriptor, JProperty property)
    {
        return queryDescriptor.Match(x => x.Field(new Field(property.Name)).Query((double)property.Value));
    }
}

/*public class NumberRangeResolver : IReadRequestFilterPropertyResolver<Person>
{
    public void Resolve(QueryDescriptor<Person> queryDescriptor, JProperty property)
    {
        queryDescriptor.Range(r => r.NumberRange(nr =>
        {
            if (property.Value.Type != JTokenType.Object)
                throw new Exception("Value is is not Object");

            var value = property.Value as JObject;

            var field = nr.Field(new Field(property.Name));
            foreach (var jProperty in value?.Properties()!)
            {
                switch (jProperty.Name)
                {
                    case "gt":
                        field.Gt((double)jProperty.Value);
                        break;
                    case "gte":
                        field.Gte((double)jProperty.Value);
                        break;
                    case "lt":
                        field.Lt((double)jProperty.Value);
                        break;
                    case "lte":
                        field.Lte((double)jProperty.Value);
                        break;
                }
            }
        }));
    }
}*/