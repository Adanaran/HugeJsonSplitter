using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace HugeJsonSplitter.Models.SystemsApi.Maps;

public class SystemMap : ClassMap<System>
{
  public SystemMap()
  {
    Map(m => m.Id64).Name(SystemsApiModelBase.PropertyNameId64);
    Map(m => m.Name).Name(SystemsApiModelBase.PropertyNameName);
    Map(m => m.Coordinates).Name(System.PropertyNameCoordinates).Convert(args => JsonConvert.SerializeObject(args.Value.Coordinates));
    Map(m => m.SystemAllegiance).Name(System.PropertyNameSystemAllegiance);
    Map(m => m.Date).Name(System.PropertyNameDate);
  }
}