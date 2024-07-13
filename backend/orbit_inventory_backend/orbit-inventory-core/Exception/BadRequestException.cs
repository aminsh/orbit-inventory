namespace orbit_inventory_core.Exception;

public class BadRequestException: System.Exception
{
    private readonly IEnumerable<string> _message;
    
    public BadRequestException(string message)
    {
        _message = new[] { message };
    }

    public BadRequestException(IEnumerable<string> message)
    {
        _message = message;
    }
}