using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HugeJsonSplitter.Models.SystemsApi;

public class SystemsApiModelBase
{
  public const string PropertyNameId64 = "id64";
  public const string PropertyNameName = "name";

  [JsonProperty(PropertyNameId64)]
  public long? Id64 { get; set; }

  [JsonProperty(PropertyNameName)]
  public string Name { get; set; }

  public async Task WriteTo(JsonTextWriter writer)
  {
    await writer.WriteStartObjectAsync();
    await WritePropertiesTo(writer);
    await writer.WriteEndObjectAsync();
  }

  protected virtual async Task WritePropertiesTo(JsonTextWriter writer)
  {
    await WriteProperty(writer, PropertyNameId64, Id64);
    await WriteProperty(writer, PropertyNameName, Name);
  }

  protected static async Task WriteProperty<TValue>(JsonTextWriter writer, string propertyName, TValue value)
  {
    await writer.WritePropertyNameAsync(propertyName);
    await writer.WriteValueAsync(value);
  }

  protected static async Task WriteProperty<TValue>(JsonTextWriter writer, string propertyName, Dictionary<string, TValue> values)
  {
    await writer.WritePropertyNameAsync(propertyName);
    if (values == null)
    {
      await writer.WriteNullAsync();
      return;
    }

    await writer.WriteStartObjectAsync();
    foreach (var (name, value) in values)
    {
      await WriteProperty(writer, name, value);
    }

    await writer.WriteEndObjectAsync();
  }

  protected static async Task WriteProperty<TValue>(JsonTextWriter writer, string propertyName, Dictionary<string, TValue>[] values)
  {
    await writer.WritePropertyNameAsync(propertyName);
    if (values == null)
    {
      await writer.WriteNullAsync();
      return;
    }

    await writer.WriteStartArrayAsync();
    foreach (var dictionary in values)
    {
      await writer.WriteStartObjectAsync();
      foreach (var (name, value) in dictionary)
      {
        await WriteProperty(writer, name, value);
      }

      await writer.WriteEndObjectAsync();
    }

    await writer.WriteEndArrayAsync();
  }
}