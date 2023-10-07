using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HugeJsonSplitter.Models.SystemsApi;

public class Element : SystemsApiModelBase
{
  public const string PropertyNameBodyId = "bodyId";
  public const string PropertyNameDiscovery = "discovery";
  public const string PropertyNameType = "type";
  public const string PropertyNameSubtype = "subType";
  public const string PropertyNameOffset = "offset";
  public const string PropertyNameParents = "parents";
  public const string PropertyNameDistancetoarrival = "distanceToArrival";
  public const string PropertyNameSurfacetemperature = "surfaceTemperature";
  public const string PropertyNameVolcanismtype = "volcanismType";
  public const string PropertyNameAtmospheretype = "atmosphereType";
  public const string PropertyNameTerraformingstate = "terraformingState";
  public const string PropertyNameOrbitalperiod = "orbitalPeriod";
  public const string PropertyNameSemimajoraxis = "semiMajorAxis";
  public const string PropertyNameOrbitaleccentricity = "orbitalEccentricity";
  public const string PropertyNameoOrbitalinclination = "orbitalInclination";
  public const string PropertyNameaArgofperiapsis = "argOfPeriapsis";
  public const string PropertyNameRotationalperiod = "rotationalPeriod";
  public const string PropertyNameRotationalperiodtidallylocked = "rotationalPeriodTidallyLocked";
  public const string PropertyNameAxialtilt = "axialTilt";
  public const string PropertyNameUpdatetime = "updateTime";
  public const string PropertyNameSystemid = "systemId";
  public const string PropertyNameSystemid64 = "systemId64";
  public const string PropertyNameSystemname = "systemName";

  [JsonProperty(PropertyNameBodyId)]
  public long? BodyId { get; set; }

  [JsonProperty(PropertyNameDiscovery)]
  public Dictionary<string, string> Discovery { get; set; }

  [JsonProperty(PropertyNameType)]
  public string Type { get; set; }

  [JsonProperty(PropertyNameSubtype)]
  public string SubType { get; set; }

  [JsonProperty(PropertyNameOffset)]
  public int Offset { get; set; }

  [JsonProperty(PropertyNameParents)]
  public Dictionary<string, string>[] Parents { get; set; }

  [JsonProperty(PropertyNameDistancetoarrival)]
  public float? DistanceToArrival { get; set; }

  [JsonProperty(PropertyNameSurfacetemperature)]
  public float? SurfaceTemperature { get; set; }

  [JsonProperty(PropertyNameVolcanismtype)]
  public string VolcanismType { get; set; }

  [JsonProperty(PropertyNameAtmospheretype)]
  public string AtmosphereType { get; set; }

  [JsonProperty(PropertyNameTerraformingstate)]
  public string TerraformingState { get; set; }

  [JsonProperty(PropertyNameOrbitalperiod)]
  public float? OrbitalPeriod { get; set; }

  [JsonProperty(PropertyNameSemimajoraxis)]
  public float? SemiMajorAxis { get; set; }

  [JsonProperty(PropertyNameOrbitaleccentricity)]
  public float? OrbitalEccentricity { get; set; }

  [JsonProperty(PropertyNameoOrbitalinclination)]
  public float? OrbitalInclination { get; set; }

  [JsonProperty(PropertyNameaArgofperiapsis)]
  public float? ArgOfPeriapsis { get; set; }

  [JsonProperty(PropertyNameRotationalperiod)]
  public float? RotationalPeriod { get; set; }

  [JsonProperty(PropertyNameRotationalperiodtidallylocked)]
  public bool? RotationalPeriodTidallyLocked { get; set; }

  [JsonProperty(PropertyNameAxialtilt)]
  public float? AxialTilt { get; set; }

  [JsonProperty(PropertyNameUpdatetime)]
  public DateTime UpdateTime { get; set; }

  [JsonProperty(PropertyNameSystemid)]
  public long SystemId { get; set; }

  [JsonProperty(PropertyNameSystemid64)]
  public long? SystemId64 { get; set; }

  [JsonProperty(PropertyNameSystemname)]
  public string SystemName { get; set; }

  protected override async Task WritePropertiesTo(JsonTextWriter writer)
  {
    await base.WritePropertiesTo(writer);
    await WriteProperty(writer, PropertyNameDiscovery, Discovery);
    await WriteProperty(writer, PropertyNameType, Type);
    await WriteProperty(writer, PropertyNameSubtype, SubType);
    await WriteProperty(writer, PropertyNameOffset, Offset);
    await WriteProperty(writer, PropertyNameParents, Parents);
    await WriteProperty(writer, PropertyNameDistancetoarrival, SurfaceTemperature);
    await WriteProperty(writer, PropertyNameVolcanismtype, VolcanismType);
    await WriteProperty(writer, PropertyNameAtmospheretype, AtmosphereType);
    await WriteProperty(writer, PropertyNameTerraformingstate, TerraformingState);
    await WriteProperty(writer, PropertyNameOrbitalperiod, OrbitalPeriod);
    await WriteProperty(writer, PropertyNameSemimajoraxis, SemiMajorAxis);
    await WriteProperty(writer, PropertyNameOrbitaleccentricity, OrbitalEccentricity);
    await WriteProperty(writer, PropertyNameoOrbitalinclination, OrbitalInclination);
    await WriteProperty(writer, PropertyNameaArgofperiapsis, ArgOfPeriapsis);
    await WriteProperty(writer, PropertyNameRotationalperiod, RotationalPeriod);
    await WriteProperty(writer, PropertyNameRotationalperiodtidallylocked, RotationalPeriodTidallyLocked);
    await WriteProperty(writer, PropertyNameAxialtilt, AxialTilt);
    await WriteProperty(writer, PropertyNameUpdatetime, UpdateTime);
    await WriteProperty(writer, PropertyNameSystemid, SystemId);
    await WriteProperty(writer, PropertyNameSystemid64, SystemId64);
    await WriteProperty(writer, PropertyNameSystemname, SystemName);
  }
}