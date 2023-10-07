using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using HugeJsonSplitter.Models.SystemsApi;
using HugeJsonSplitter.Models.SystemsApi.Maps;

namespace HugeJsonSplitter.Writers;

public class CsvWriter<TElement> : WriterBase<TElement>
  where TElement : SystemsApiModelBase
{
  public CsvWriter(string outputDir, string fileName, int maxLineCount)
    : base(outputDir, fileName, maxLineCount)
  {
  }

  protected override async Task Write(BlockingCollection<TElement> queue)
  {
    await using var streamWriter = File.CreateText(Path.Combine(outputDir, $"{fileName}{partCount}.csv"));
    await using var csvWriter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture)
    {
      Escape = '\\'
    });
    csvWriter.Context.RegisterClassMap<SystemMap>();
    csvWriter.Context.RegisterClassMap<BodyMap>();
    csvWriter.Context.RegisterClassMap<StarMap>();

    csvWriter.WriteHeader<TElement>();
    await csvWriter.NextRecordAsync();

    while (!queue.IsCompleted)
    {
      if (queue.TryTake(out var element))
      {
        csvWriter.WriteRecord(element);
        await csvWriter.NextRecordAsync();
      }
    }
  }
}