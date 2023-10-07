using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using HugeJsonSplitter.Models.SystemsApi;
using Newtonsoft.Json;

namespace HugeJsonSplitter.Writers;

public class JsonWriter<TElement> : WriterBase<TElement>
  where TElement : SystemsApiModelBase
{
  public JsonWriter(string outputDir, string fileName, int maxLineCount)
    : base(outputDir, fileName, maxLineCount)
  {
  }

  protected override async Task Write(BlockingCollection<TElement> queue)
  {
    await using var streamWriter = File.CreateText(Path.Combine(outputDir, $"{fileName}{partCount}.json"));
    using var jsonTextWriter = new JsonTextWriter(streamWriter);
    jsonTextWriter.Formatting = Formatting.Indented;
    await jsonTextWriter.WriteStartArrayAsync();
    while (!queue.IsCompleted)
    {
      if (queue.TryTake(out var element))
      {
        await element.WriteTo(jsonTextWriter);
      }
    }

    await jsonTextWriter.WriteEndArrayAsync();
  }
}