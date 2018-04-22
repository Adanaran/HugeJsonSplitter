using Newtonsoft.Json;

namespace HugeJsonSplitter
{
  public class Body : Element
  {
    [JsonProperty("isLandable")]
    public bool? IsLandable { get; set; }

    [JsonProperty("gravity")]
    public float? Gravity { get; set; }

    [JsonProperty("earthMasses")]
    public float? EarthMasses { get; set; }

    [JsonProperty("radius")]
    public float? Radius { get; set; }

    [JsonProperty("surfacePressure")]
    public float? SurfacePressure { get; set; }

    [JsonProperty("atmosphereComposition")]
    public object AtmosphereComposition { get; set; }

    [JsonProperty("rings")]
    public object Rings { get; set; }

    [JsonProperty("materials")]
    public object Materials { get; set; }
  }
}