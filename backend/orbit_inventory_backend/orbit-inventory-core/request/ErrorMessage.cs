

namespace orbit_inventory_core.request;

public record ErrorMessage(string Message, string? Field = null);

public static class ErrorMessageExtensions
{
    /*public static IEnumerable<ErrorMessage> ToErrorMessage(this ModelStateDictionary modelState)
    {
        return modelState
            .Where(ms => ms.Value is { Errors.Count: > 0 })
            .SelectMany(ms => ms.Value.Errors, (ms, e) =>
                new ErrorMessage(e.ErrorMessage, ms.Key.ToLower()));
    }*/
}