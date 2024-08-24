using orbit_inventory_core.request;

namespace orbit_inventory_core.Exception;

public class BadRequestException : System.Exception
{
    public readonly IEnumerable<ErrorMessage> Messages;

    public BadRequestException(string message, string? field = null)
    {
        Messages = [new ErrorMessage(message, field?.ToLower())];
    }

    public BadRequestException(IEnumerable<ErrorMessage> message)
    {
        Messages = message.Select(msg => msg with { Field = msg.Field?.ToLower() });
    }
}