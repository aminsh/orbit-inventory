namespace orbit_inventory_core.Exception;

public class BadRequestException: System.Exception
{
    public readonly IEnumerable<string> Messages;
    
    public BadRequestException(string message)
    {
        Messages = new[] { message };
    }

    public BadRequestException(IEnumerable<string> message)
    {
        Messages = message;
    }
}