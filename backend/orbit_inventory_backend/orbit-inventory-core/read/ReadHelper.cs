using orbit_inventory_core.Utils;

namespace orbit_inventory_core.read;

public static class ReadHelper
{
    public static string GetIndexNameOf<TView>()
    {
        return $"{Environment.GetEnvironmentVariable("ELASTIC_INDEX_PREFIX")}-{typeof(TView).Name.PascalToKebabCase()}";
    }
}