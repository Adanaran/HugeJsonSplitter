using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace HugeJsonSplitter.Models.SystemsApi.Maps;

public class StarMap : ClassMap<Star>
{
  public StarMap()
  {
    Map(m => m.Id64).Name(SystemsApiModelBase.PropertyNameId64);
    Map(m => m.Name).Name(SystemsApiModelBase.PropertyNameName);
    Map(m => m.Type).Name(Element.PropertyNameType);
    Map(m => m.SubType).Name(Element.PropertyNameSubtype);
    Map(m => m.Parents).Name(Element.PropertyNameParents).Convert(args => JsonConvert.SerializeObject(args.Value.Parents));
    Map(m => m.DistanceToArrival).Name(Element.PropertyNameDistancetoarrival);
    Map(m => m.IsMainStar).Name(Star.PropertyNameIsmainstar);
    Map(m => m.IsScoopable).Name(Star.PropertyNameIsscoopable);
    Map(m => m.Age).Name(Star.PropertyNameAge);
    Map(m => m.Luminosity).Name(Star.PropertyNameLuminosity);
    Map(m => m.AbsoluteMagnitude).Name(Star.PropertyNameAbsolutemagnitude);
    Map(m => m.SolarMasses).Name(Star.PropertyNameSolarmasses);
    Map(m => m.SolarRadius).Name(Star.PropertyNameSolarradius);
    Map(m => m.SurfaceTemperature).Name(Element.PropertyNameSurfacetemperature);
    Map(m => m.OrbitalPeriod).Name(Element.PropertyNameOrbitalperiod);
    Map(m => m.SemiMajorAxis).Name(Element.PropertyNameSemimajoraxis);
    Map(m => m.OrbitalEccentricity).Name(Element.PropertyNameOrbitaleccentricity);
    Map(m => m.OrbitalInclination).Name(Element.PropertyNameoOrbitalinclination);
    Map(m => m.ArgOfPeriapsis).Name(Element.PropertyNameaArgofperiapsis);
    Map(m => m.RotationalPeriod).Name(Element.PropertyNameRotationalperiod);
    Map(m => m.RotationalPeriodTidallyLocked).Name(Element.PropertyNameRotationalperiodtidallylocked);
    Map(m => m.AxialTilt).Name(Element.PropertyNameAxialtilt);
    Map(m => m.Belts).Name(Star.PropertyNameBelts).Convert(args => JsonConvert.SerializeObject(args.Value.Belts));
    Map(m => m.UpdateTime).Name(Element.PropertyNameUpdatetime);
    Map(m => m.SystemId64).Name(Element.PropertyNameSystemid64);
    Map(m => m.SystemName).Name(Element.PropertyNameSystemname);
  }
}