using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HugeJsonSplitter.Models.SystemsApi;

public class System : SystemsApiModelBase
{
  public const string PropertyNameCoordinates = "coords";
  public const string PropertyNameDate = "date";
  public const string PropertyNameSystemAllegiance = "systemAllegiance";

  [JsonProperty(PropertyNameCoordinates)]
  public Dictionary<string, decimal> Coordinates { get; set; }

  [JsonProperty(PropertyNameDate)]
  public string Date { get; set; }

  public IList<SystemsApiModelBase> Bodies { get; set; }

  [JsonProperty(PropertyNameSystemAllegiance)]
  public string SystemAllegiance { get; set; }

  protected override async Task WritePropertiesTo(JsonTextWriter writer)
  {
    await base.WritePropertiesTo(writer);
    await WriteProperty(writer, PropertyNameCoordinates, Coordinates);
  }
}