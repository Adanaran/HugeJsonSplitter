using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace HugeJsonSplitter.Models.SystemsApi.Maps;

public class BodyMap : ClassMap<Body>
{
  public BodyMap()
  {
    Map(m => m.Id64).Name(SystemsApiModelBase.PropertyNameId64);
    Map(m => m.Name).Name(SystemsApiModelBase.PropertyNameName);
    Map(m => m.Type).Name(Element.PropertyNameType);
    Map(m => m.SubType).Name(Element.PropertyNameSubtype);
    Map(m => m.Parents).Name(Element.PropertyNameParents).Convert(args => JsonConvert.SerializeObject(args.Value.Parents));
    Map(m => m.DistanceToArrival).Name(Element.PropertyNameDistancetoarrival);
    Map(m => m.IsLandable).Name(Body.PropertyNameIslandable);
    Map(m => m.Gravity).Name(Body.PropertyNameGravity);
    Map(m => m.EarthMasses).Name(Body.PropertyNameEarthmasses);
    Map(m => m.Radius).Name(Body.PropertyNameRadius);
    Map(m => m.SurfaceTemperature).Name(Element.PropertyNameSurfacetemperature);
    Map(m => m.SurfacePressure).Name(Body.PropertyNameSurfacepressure);
    Map(m => m.VolcanismType).Name(Element.PropertyNameVolcanismtype);
    Map(m => m.AtmosphereType).Name(Element.PropertyNameAtmospheretype);
    Map(m => m.AtmosphereComposition).Name(Body.PropertyNameAtmospherecomposition).Convert(args => JsonConvert.SerializeObject(args.Value.AtmosphereComposition));
    Map(m => m.SolidComposition).Name(Body.PropertyNameSolidComposition);
    Map(m => m.TerraformingState).Name(Element.PropertyNameTerraformingstate);
    Map(m => m.OrbitalPeriod).Name(Element.PropertyNameOrbitalperiod);
    Map(m => m.SemiMajorAxis).Name(Element.PropertyNameSemimajoraxis);
    Map(m => m.OrbitalEccentricity).Name(Element.PropertyNameOrbitaleccentricity);
    Map(m => m.OrbitalInclination).Name(Element.PropertyNameoOrbitalinclination);
    Map(m => m.ArgOfPeriapsis).Name(Element.PropertyNameaArgofperiapsis);
    Map(m => m.RotationalPeriod).Name(Element.PropertyNameRotationalperiod);
    Map(m => m.RotationalPeriodTidallyLocked).Name(Element.PropertyNameRotationalperiodtidallylocked);
    Map(m => m.AxialTilt).Name(Element.PropertyNameAxialtilt);
    Map(m => m.Rings).Name(Body.PropertyNameRings).Convert(args => JsonConvert.SerializeObject(args.Value.Rings));
    Map(m => m.Materials).Name(Body.PropertyNameMaterials).Convert(args => JsonConvert.SerializeObject(args.Value.Materials));
    Map(m => m.UpdateTime).Name(Element.PropertyNameUpdatetime);
    Map(m => m.SystemId64).Name(Element.PropertyNameSystemid64);
    Map(m => m.SystemName).Name(Element.PropertyNameSystemname);
  }
}