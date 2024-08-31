using GraphQL;
using GraphQL.Types;
using GraphQLParser.AST;
using Microsoft.Extensions.DependencyInjection;

namespace orbit_inventory_graphql;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class PersonType : ObjectGraphType<Person>
{
    public PersonType()
    {
        Field(x => x.Id);
        Field(x => x.Name);
    }
}

public class PersonData
{
    public static List<Person> People { get; set; }

    static PersonData()
    {
        People =
        [
            new Person { Id = 1, Name = "amin" },
            new Person { Id = 2, Name = "Ali" }
        ];
    }
}


public class PersonInput : InputObjectGraphType
{
    public PersonInput()
    {
        Field<StringGraphType>("name");
    }
}
