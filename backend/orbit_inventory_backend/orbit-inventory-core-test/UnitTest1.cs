using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace orbit_inventory_core_test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }
    
    

    [Test]
    public void Test1()
    {
      var error = new ErrorMessage("Test Error", "Name");
    }
}

public record ErrorMessage(string Message, string Field);