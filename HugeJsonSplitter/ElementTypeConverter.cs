using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HugeJsonSplitter
{
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

      var typeProperty = jObject.Property(Element.PropertyNameType);
      if (typeProperty == null)
      {
        WriteDebugInfo(jObject);
        return null;
      }

      var elementType = typeProperty.Value.Value<string>();

      Type targetType;
      switch (elementType)
      {
        case "Planet":
        case "Body":
          targetType = typeof(Body);
          break;
        case "Star":
          targetType = typeof(Star);
          break;
        default:
          WriteDebugInfo(jObject);
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

    private static void WriteDebugInfo(JObject jObject)
    {
      Console.WriteLine(jObject.ToString());
    }

    public override bool CanConvert(Type objectType)
    {
      return typeof(Element).IsAssignableFrom(objectType);
    }
  }
}