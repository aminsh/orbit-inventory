using GraphQL;
using GraphQL.Types;
using GraphQLParser.AST;
using Nest;

namespace orbit_inventory_main.gql;

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

public class PersonQuery : ObjectGraphType<object>
{
    public PersonQuery()
    {
        Field<ListGraphType<PersonType>>("people").Resolve(ctx => PersonData.People);
    }
}

public class PersonInput : InputObjectGraphType
{
    public PersonInput()
    {
        Field<StringGraphType>("name");
    }
}

public class VoidType : ScalarGraphType
{
    public VoidType()
    {
        Name = "Void";
    }
    
    public override object? ParseLiteral(GraphQLValue value)
    {
        return null;
    }

    public override object? ParseValue(object? value)
    {
        return null;
    }

    public override object? Serialize(object? value)
    {
        return null;
    }
}

public sealed class PersonMutation : ObjectGraphType
{
    public PersonMutation()
    {
        Field<PersonType>("addPerson")
            .Arguments(new QueryArguments(
                new QueryArgument<PersonInput> { Name = "input" }
            ))
            .Resolve(context =>
            {
                var id = PersonData.People.Max(e => e.Id) + 1;
                var input = context.GetArgument<Person>("input");

                var newPerson = new Person { Id = id, Name = input.Name };

                PersonData.People.Add(newPerson);

                return newPerson;
            });

        Field<VoidType>("updatePerson")
            .Arguments(new QueryArguments(
                new QueryArgument<IdGraphType> { Name = "id" },
                new QueryArgument<PersonInput> { Name = "input" }
            ))
            .Resolve(context =>
            {
                var id = context.GetArgument<int>("id");
                var person = PersonData.People.First(p => p.Id == id);
                var input = context.GetArgument<Person>("input");
                person.Name = input.Name;
                return null;
            });
    }
}

public class PersonSchema : Schema
{
    public PersonSchema(IServiceProvider serviceProvider)
    {
        this.Authorize();
        Query = serviceProvider.GetRequiredService<PersonQuery>();
        Mutation = serviceProvider.GetRequiredService<PersonMutation>();
    }
}