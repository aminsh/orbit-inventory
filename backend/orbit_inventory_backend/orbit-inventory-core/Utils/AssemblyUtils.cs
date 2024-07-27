using System.Reflection;

namespace orbit_inventory_core.Utils;

public static class AssemblyUtils
{
    public static Assembly Get(string name)
    {
        var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        if (assemblyFolder == null)
            throw new ArgumentException(null, nameof(name));
        
        var files = Directory.GetFiles(assemblyFolder, $"*{name}.dll");

        var fileName = files.FirstOrDefault(e => e.Contains(name));

        if (string.IsNullOrEmpty(fileName))
            throw new System.Exception("Assembly name not exists");

        return Assembly.LoadFrom(fileName);
    }
}