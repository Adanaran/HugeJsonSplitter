using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HugeJsonSplitter.Models.SystemsApi;

public class Body : Element
{
  public const string PropertyNameIslandable = "isLandable";
  public const string PropertyNameGravity = "gravity";
  public const string PropertyNameEarthmasses = "earthMasses";
  public const string PropertyNameRadius = "radius";
  public const string PropertyNameSurfacepressure = "surfacePressure";
  public const string PropertyNameAtmospherecomposition = "atmosphereComposition";
  public const string PropertyNameSolidComposition = "solidComposition";
  public const string PropertyNameRings = "rings";
  public const string PropertyNameMaterials = "materials";

  [JsonProperty(PropertyNameIslandable)]
  public bool? IsLandable { get; set; }

  [JsonProperty(PropertyNameGravity)]
  public float? Gravity { get; set; }

  [JsonProperty(PropertyNameEarthmasses)]
  public float? EarthMasses { get; set; }

  [JsonProperty(PropertyNameRadius)]
  public float? Radius { get; set; }

  [JsonProperty(PropertyNameSurfacepressure)]
  public float? SurfacePressure { get; set; }

  [JsonProperty(PropertyNameAtmospherecomposition)]
  public Dictionary<string, float> AtmosphereComposition { get; set; }

  [JsonProperty(PropertyNameSolidComposition)]
  public Dictionary<string, float> SolidComposition { get; set; }

  [JsonProperty(PropertyNameRings)]
  public Dictionary<string, JToken>[] Rings { get; set; }

  [JsonProperty(PropertyNameMaterials)]
  public Dictionary<string, float> Materials { get; set; }

  protected override async Task WritePropertiesTo(JsonTextWriter writer)
  {
    await base.WritePropertiesTo(writer);
    await WriteProperty(writer, PropertyNameIslandable, IsLandable);
    await WriteProperty(writer, PropertyNameGravity, Gravity);
    await WriteProperty(writer, PropertyNameEarthmasses, EarthMasses);
    await WriteProperty(writer, PropertyNameRadius, Radius);
    await WriteProperty(writer, PropertyNameSurfacepressure, SurfacePressure);
    await WriteProperty(writer, PropertyNameAtmospherecomposition, AtmosphereComposition);
    await WriteProperty(writer, PropertyNameRings, Rings);
    await WriteProperty(writer, PropertyNameMaterials, Materials);
  }
}