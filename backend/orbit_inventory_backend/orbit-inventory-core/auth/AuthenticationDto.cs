namespace orbit_inventory_core.auth;

public class SignUpDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
}

public class SignInDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}