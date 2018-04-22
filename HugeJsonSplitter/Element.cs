using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HugeJsonSplitter
{
  public class Element
  {
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("id64")]
    public long? Id64 { get; set; }

    [JsonProperty("bodyId")]
    public long? BodyId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("discovery")]
    public object Discovery { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("subType")]
    public string SubType { get; set; }

    [JsonProperty("offset")]
    public int Offset { get; set; }

    [JsonProperty("parents")]
    public object Parents { get; set; }

    [JsonProperty("distanceToArrival")]
    public float? DistanceToArrival { get; set; }

    [JsonProperty("surfaceTemperature")]
    public float? SurfaceTemperature { get; set; }

    [JsonProperty("volcanismType")]
    public string VolcanismType { get; set; }

    [JsonProperty("atmosphereType")]
    public string AtmosphereType { get; set; }

    [JsonProperty("terraformingState")]
    public string TerraformingState { get; set; }

    [JsonProperty("orbitalPeriod")]
    public float? OrbitalPeriod { get; set; }

    [JsonProperty("semiMajorAxis")]
    public float? SemiMajorAxis { get; set; }

    [JsonProperty("orbitalEccentricity")]
    public float? OrbitalEccentricity { get; set; }

    [JsonProperty("orbitalInclination")]
    public float? OrbitalInclination { get; set; }

    [JsonProperty("argOfPeriapsis")]
    public float? ArgOfPeriapsis { get; set; }

    [JsonProperty("rotationalPeriod")]
    public float? RotationalPeriod { get; set; }

    [JsonProperty("rotationalPeriodTidallyLocked")]
    public bool? RotationalPeriodTidallyLocked { get; set; }

    [JsonProperty("axialTilt")]
    public float? AxialTilt { get; set; }

    [JsonProperty("updateTime")]
    public DateTime UpdateTime { get; set; }

    [JsonProperty("systemId")]
    public long SystemId { get; set; }

    [JsonProperty("systemId64")]
    public long SystemId64 { get; set; }

    [JsonProperty("systemName")]
    public string SystemName { get; set; }
    public override string ToString()
    {
      return JsonConvert.SerializeObject(this, new JsonSerializerSettings
      {
        Converters = new List<JsonConverter> { new ElementTypeConverter() },
        DefaultValueHandling = DefaultValueHandling.Include
      });
    }
  }
}