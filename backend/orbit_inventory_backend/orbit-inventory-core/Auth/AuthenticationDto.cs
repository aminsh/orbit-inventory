namespace orbit_inventory_core.Auth;

public class SignupDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
}

public class SigninDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}