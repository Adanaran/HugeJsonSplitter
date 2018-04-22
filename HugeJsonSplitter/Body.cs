using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HugeJsonSplitter
{
  public class Body : Element
  {
    public const string PropertyNameIslandable = "isLandable";
    private const string PropertyNameGravity = "gravity";
    private const string PropertyNameEarthmasses = "earthMasses";
    private const string PropertyNameRadius = "radius";
    private const string PropertyNameSurfacepressure = "surfacePressure";
    private const string PropertyNameAtmospherecomposition = "atmosphereComposition";
    private const string PropertyNameRings = "rings";
    private const string PropertyNameMaterials = "materials";

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

    [JsonProperty(PropertyNameRings)]
    public Dictionary<string, string>[] Rings { get; set; }

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
}