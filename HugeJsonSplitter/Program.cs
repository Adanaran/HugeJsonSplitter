using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HugeJsonSplitter.Models.SystemsApi;
using HugeJsonSplitter.Writers;
using Newtonsoft.Json;

namespace HugeJsonSplitter;

internal class Program
{
  public static async Task Main(string[] args)
  {
    var stopwatch = Stopwatch.StartNew();

    var arguments = args.ToList();
    var lineCount = GetLineCount(arguments);
    var outputDirectory = EnsureOutputDirectory(arguments);
    var inputFile = EnsureInputFile(arguments);
    var outputType = EnsureOutputType(arguments);
    if (inputFile == null)
    {
      return;
    }

    if (inputFile.StartsWith("http", StringComparison.OrdinalIgnoreCase))
    {
      using var client = new HttpClient();
      using var response = await client.GetAsync(inputFile, HttpCompletionOption.ResponseHeadersRead);
      using var stream = await response.Content.ReadAsStreamAsync();
      using var streamReader = new StreamReader(stream);
      await Chunk(streamReader, outputDirectory, lineCount, outputType);
    }
    else
    {
      using var reader = File.OpenText(inputFile);
      await Chunk(reader, outputDirectory, lineCount, outputType);
    }

    stopwatch.Stop();
    Console.WriteLine($"Chunking took: {stopwatch.Elapsed:g}");
  }

  private static async Task Chunk(StreamReader reader, string outputDirectory, int lineCount, OutputType outputType)
  {
    var bodyWriter = CreateWriter<Body>(outputType, outputDirectory, "bodies", lineCount);
    var starWriter = CreateWriter<Star>(outputType, outputDirectory, "stars", lineCount);
    var starSystemWriter = CreateWriter<Models.SystemsApi.System>(outputType, outputDirectory, "systemsWithCoordinates", lineCount);
    bodyWriter.Start();
    starWriter.Start();
    starSystemWriter.Start();

    var jsonSerializer = new JsonSerializer();
    jsonSerializer.Converters.Add(new ElementTypeConverter());
    jsonSerializer.DefaultValueHandling = DefaultValueHandling.Populate;

    using (var jsonTextReader = new JsonTextReader(reader))
    {
      while (await jsonTextReader.ReadAsync())
      {
        if (jsonTextReader.TokenType == JsonToken.StartObject)
        {
          var element = jsonSerializer.Deserialize<SystemsApiModelBase>(jsonTextReader);
          switch (element)
          {
            case Body body:
              bodyWriter.Add(body);
              break;
            case Star star:
              starWriter.Add(star);
              break;
            case Models.SystemsApi.System starSystemWithCoordinates:
              starSystemWriter.Add(starSystemWithCoordinates);
              foreach (var systemsApiModelBase in starSystemWithCoordinates.Bodies)
              {
                switch (systemsApiModelBase)
                {
                  case Body body:
                    bodyWriter.Add(body);
                    break;
                  case Star star:
                    starWriter.Add(star);
                    break;
                }
              }

              break;
          }
        }
      }
    }

    await starSystemWriter.End();
    await bodyWriter.End();
    await starWriter.End();
  }

  private static WriterBase<TElement> CreateWriter<TElement>(OutputType outputType, string outputDirectory, string fileName, int lineCount)
    where TElement : SystemsApiModelBase
  {
    return outputType switch
    {
      OutputType.Json => new JsonWriter<TElement>(outputDirectory, fileName, lineCount),
      OutputType.Csv => new CsvWriter<TElement>(outputDirectory, fileName, lineCount),
      _ => throw new ArgumentOutOfRangeException(nameof(outputType), outputType, null)
    };
  }

  private static int GetLineCount(IList<string> arguments)
  {
    var linecountIndex = arguments.IndexOf("--linecount");
    if (linecountIndex == -1 || !int.TryParse(arguments[linecountIndex + 1], out var lineCount))
    {
      lineCount = 1_000_000;
    }

    return lineCount;
  }

  private static string EnsureInputFile(IList<string> arguments)
  {
    var inputIndex = arguments.IndexOf("--input");
    if (inputIndex == -1)
    {
      Console.WriteLine("Please add an input file via --input path/to/file");
      Console.ReadKey();
      return null;
    }

    var inputFile = arguments[inputIndex + 1];

    if (!inputFile.StartsWith("http", StringComparison.OrdinalIgnoreCase))
    {
      if (!File.Exists(inputFile))
      {
        Console.WriteLine("Input file not found.");
        Console.ReadKey();
        return null;
      }
    }

    return inputFile;
  }

  private static string EnsureOutputDirectory(IList<string> arguments)
  {
    var outputIndex = arguments.IndexOf("--outputdir");
    var outputDir = outputIndex == -1 ? AppDomain.CurrentDomain.BaseDirectory : arguments[outputIndex + 1];

    if (!Directory.Exists(outputDir))
    {
      Directory.CreateDirectory(outputDir);
    }

    return outputDir;
  }

  private static OutputType EnsureOutputType(IList<string> arguments)
  {
    var outputIndex = arguments.IndexOf("--outputtype");
    if (outputIndex == -1 || !Enum.TryParse<OutputType>(arguments[outputIndex + 1], out var outputType))
    {
      return OutputType.Json;
    }

    return outputType;
  }
}