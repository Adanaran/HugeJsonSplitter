using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HugeJsonSplitter
{
  public class StarSystemWithCoordinates : JsonObjectBase
  {
    public const string PropertyNameCoordinates = "coords";

    [JsonProperty(PropertyNameCoordinates)]
    public Dictionary<string, decimal> Coordinates { get; set; }

    protected override async Task WritePropertiesTo(JsonTextWriter writer)
    {
      await base.WritePropertiesTo(writer);
      await WriteProperty(writer, PropertyNameCoordinates, Coordinates);
    }
  }
}