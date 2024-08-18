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
        JObject obj = JObject.Parse(@"
        {
          ""from"": 0,
          ""size"": 10,
          ""query"": {
            ""bool"": {
              ""should"": [
                {
                  ""wildcard"": {
                    ""name"": ""Joseph*""
                  }
                },
                {
                  ""wildcard"": {
                    ""name"": ""Hassan""
                  }
                }
              ]
            }
          }
        }
        ");

        var numberTypes = new List<JTokenType> { JTokenType.Float, JTokenType.Integer };
        var isNumber = numberTypes.Any(t => t == obj["query"]!.Type);
    }
}