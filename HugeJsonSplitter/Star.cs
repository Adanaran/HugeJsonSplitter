using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HugeJsonSplitter
{
  public class Star : Element
  {
    private const string PropertyNameIsmainstar = "isMainStar";
    private const string PropertyNameIsscoopable = "isScoopable";
    private const string PropertyNameAge = "age";
    private const string PropertyNameLuminosity = "luminosity";
    private const string PropertyNameAbsolutemagnitude = "absoluteMagnitude";
    private const string PropertyNameSolarmasses = "solarMasses";
    private const string PropertyNameSolarradius = "solarRadius";
    private const string PropertyNameBelts = "belts";

    [JsonProperty(PropertyNameIsmainstar)]
    public bool IsMainStar { get; set; }

    [JsonProperty(PropertyNameIsscoopable)]
    public bool IsScoopable { get; set; }

    [JsonProperty(PropertyNameAge)]
    public long? Age { get; set; }

    [JsonProperty(PropertyNameLuminosity)]
    public string Luminosity { get; set; }

    [JsonProperty(PropertyNameAbsolutemagnitude)]
    public float? AbsoluteMagnitude { get; set; }

    [JsonProperty(PropertyNameSolarmasses)]
    public float? SolarMasses { get; set; }

    [JsonProperty(PropertyNameSolarradius)]
    public float? SolarRadius { get; set; }

    [JsonProperty(PropertyNameBelts)]
    public Dictionary<string, string>[] Belts { get; set; }

    protected override async Task WritePropertiesTo(JsonTextWriter writer)
    {
      await base.WritePropertiesTo(writer);
      await WriteProperty(writer, PropertyNameIsmainstar, IsMainStar);
      await WriteProperty(writer, PropertyNameIsscoopable, IsScoopable);
      await WriteProperty(writer, PropertyNameAge, Age);
      await WriteProperty(writer, PropertyNameLuminosity, Luminosity);
      await WriteProperty(writer, PropertyNameAbsolutemagnitude, AbsoluteMagnitude);
      await WriteProperty(writer, PropertyNameSolarmasses, SolarMasses);
      await WriteProperty(writer, PropertyNameSolarradius, SolarRadius);
      await WriteProperty(writer, PropertyNameBelts, Belts);
    }
  }
}