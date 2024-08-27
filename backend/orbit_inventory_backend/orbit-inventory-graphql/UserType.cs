using GraphQL.Types;
using orbit_inventory_core.auth;
using orbit_inventory_dto;

namespace orbit_inventory_graphql;

public sealed class UserType : ObjectGraphType<UserView>
{
    public UserType()
    {
        Field(f => f.Id);
        Field(f => f.Name);
        Field(f => f.Email);
    }
}

public sealed class AuthenticationResponseType : ObjectGraphType<AuthenticationResponse>
{
    public AuthenticationResponseType()
    {
        Field(f => f.AccessToken);
        Field(f => f.TokenType);
    }
}

public sealed class SignUpInputType : InputObjectGraphType<SignUpDto>
{
    public SignUpInputType()
    {
        Field(f => f.Email);
        Field(f => f.Name);
        Field(f => f.Password);
    }
}

public sealed class SignInInputType : InputObjectGraphType<SignInDto>
{
    public SignInInputType()
    {
        Field(f => f.Email);
        Field(f => f.Password);
    }
}

public sealed class UpdateProfileInputType : AutoRegisteringInputObjectGraphType<UserUpdateDto>
{
}