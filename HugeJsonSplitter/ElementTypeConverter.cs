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
      var elementType = (string) jObject.Property("type");

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
          throw new NotSupportedException($"Element type '{elementType}' is not supported.");
      }

      var target = Activator.CreateInstance(targetType);
      serializer.Populate(jObject.CreateReader(), target);

      return target;
    }

    public override bool CanConvert(Type objectType)
    {
      return typeof(Element).IsAssignableFrom(objectType);
    }
  }
}