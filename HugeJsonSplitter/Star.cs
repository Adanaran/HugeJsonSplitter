using Newtonsoft.Json;

namespace HugeJsonSplitter
{
  public class Star : Element
  {

    [JsonProperty("isMainStar")]
    public bool IsMainStar { get; set; }

    [JsonProperty("isScoopable")]
    public bool IsScoopable { get; set; }

    [JsonProperty("age")]
    public long? Age { get; set; }

    [JsonProperty("luminosity")]
    public string Luminosity { get; set; }

    [JsonProperty("absoluteMagnitude")]
    public float? AbsoluteMagnitude { get; set; }

    [JsonProperty("solarMasses")]
    public float? SolarMasses { get; set; }

    [JsonProperty("solarRadius")]
    public float? SolarRadius { get; set; }

    [JsonProperty("belts")]
    public object Belts { get; set; }

  }
}