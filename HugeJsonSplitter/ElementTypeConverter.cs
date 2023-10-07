using System;
using HugeJsonSplitter.Models.SystemsApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HugeJsonSplitter;

public class ElementTypeConverter : JsonConverter
{
  public override bool CanWrite => false;

  public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
  {
    throw new NotSupportedException();
  }

  public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
  {
    var jObject = JObject.Load(reader);

    var targetType = GetTargetType(jObject);
    if (targetType == null)
    {
      WriteDebugInfo(jObject);
      return null;
    }

    if (targetType == typeof(int))
    {
      return null;
    }

    try
    {
      var target = Activator.CreateInstance(targetType);
      serializer.Populate(jObject.CreateReader(), target);

      return target;
    }
    catch (Exception e)
    {
      WriteDebugInfo(jObject);
      Console.WriteLine(e);
    }

    return null;
  }

  public override bool CanConvert(Type objectType)
  {
    return typeof(SystemsApiModelBase).IsAssignableFrom(objectType);
  }

  private static Type GetTargetType(JObject jObject)
  {
    var coordinatesProperty = jObject.Property(Models.SystemsApi.System.PropertyNameCoordinates);
    if (coordinatesProperty != null)
    {
      return typeof(Models.SystemsApi.System);
    }

    var typeProperty = jObject.Property(Element.PropertyNameType);
    if (typeProperty == null)
    {
      return null;
    }

    var elementType = typeProperty.Value.Value<string>();

    Type targetType = null;
    switch (elementType)
    {
      case "Planet":
      case "Body":
        targetType = typeof(Body);
        break;
      case "Star":
        targetType = typeof(Star);
        break;
      case "Barycentre":
        targetType = typeof(int);
        break;
      default:
        WriteDebugInfo(jObject);
        break;
    }

    return targetType;
  }

  private static void WriteDebugInfo(JObject jObject)
  {
    Console.WriteLine(jObject.ToString());
  }
}