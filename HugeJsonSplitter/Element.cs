using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HugeJsonSplitter
{
  public class Element : JsonObjectBase
  {
    private const string PropertyNameBodyId = "bodyId";
    private const string PropertyNameDiscovery = "discovery";
    public const string PropertyNameType = "type";
    private const string PropertyNameSubtype = "subType";
    private const string PropertyNameOffset = "offset";
    private const string PropertyNameParents = "parents";
    private const string PropertyNameDistancetoarrival = "distanceToArrival";
    private const string PropertyNameSurfacetemperature = "surfaceTemperature";
    private const string PropertyNameVolcanismtype = "volcanismType";
    private const string PropertyNameAtmospheretype = "atmosphereType";
    private const string PropertyNameTerraformingstate = "terraformingState";
    private const string PropertyNameOrbitalperiod = "orbitalPeriod";
    private const string PropertyNameSemimajoraxis = "semiMajorAxis";
    private const string PropertyNameOrbitaleccentricity = "orbitalEccentricity";
    private const string PropertyNameoOrbitalinclination = "orbitalInclination";
    private const string PropertyNameaArgofperiapsis = "argOfPeriapsis";
    private const string PropertyNameRotationalperiod = "rotationalPeriod";
    private const string PropertyNameRotationalperiodtidallylocked = "rotationalPeriodTidallyLocked";
    private const string PropertyNameAxialtilt = "axialTilt";
    private const string PropertyNameUpdatetime = "updateTime";
    private const string PropertyNameSystemid = "systemId";
    private const string PropertyNameSystemid64 = "systemId64";
    private const string PropertyNameSystemname = "systemName";

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
}