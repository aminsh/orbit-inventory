using Nest;

namespace orbit_inventory_core_test;

[TestFixture]
public class NestTest
{
    [Test]
    public async Task Test()
    {
        var client = new ElasticClient(new Uri("http://localhost:1004"));

        var response = await client.SearchAsync<Customer>(s => s
            .Index("test-customer")
            .Query(q => q
                .Bool(b => b
                    .Must(
                        mu => mu
                            .Match(t => t
                                .Field(f => f.Id)
                                .Query("1")
                            ),
                        mu => mu
                            .Match(t => t
                                .Field(f => f.CreatedBy.Id)
                                .Query("18")
                            )
                    )
                )
            )
        );


        /*await client.Indices.CreateAsync("test-customer", c => c
            .Map<Customer>(m => m
                .Properties(p => p
                    .Number(f => f
                        .Name(n => n.Id)
                        .Type(NumberType.Integer)
                    )
                    .Keyword(f => f
                        .Name(n => n.Name)
                    )
                    .Object<User>(u => u
                        .Name(n => n.CreatedBy)
                        .Properties(pu => pu
                            .Number(f => f
                                .Name(n => n.Id)
                                .Type(NumberType.Integer)
                            )
                            .Keyword(f => f
                                .Name(n => n.Name)
                            )
                        )
                    )
                )
            )
        );*/

        /*var customers = new List<Customer>
        {
            new Customer
            {
                Id = 1,
                Name = "Customer1",
                CreatedBy = new User
                {
                    Id = 11,
                    Name = "User1"
                }
            },
            new Customer
            {
                Id = 2,
                Name = "Customer12",
                CreatedBy = new User
                {
                    Id = 22,
                    Name = "User2"
                }
            }
        };

        foreach (var customer in customers)
        {
            await client.CreateAsync(customer, u => u.Index("test-customer"));
        }
        */
    }
}

public class Customer
{
    public long Id { get; set; }
    public string Name { get; set; }
    public User CreatedBy { get; set; }
}

public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
}