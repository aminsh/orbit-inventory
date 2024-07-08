namespace orbit_inventory_domain.UserSection;

public class UserDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
}

public class UserSigninDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}