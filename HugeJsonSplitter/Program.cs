using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HugeJsonSplitter
{
  internal class Program
  {
    public static async Task Main(string[] args)
    {
      var stopwatch = Stopwatch.StartNew();

      var arguments = args.ToList();
      var lineCount = GetLineCount(arguments);
      var outputDirectory = EnsureOutputDirectory(arguments);
      var inputFile = EnsureInputFile(arguments);
      if (inputFile == null)
      {
        return;
      }

      if (inputFile.StartsWith("http", StringComparison.OrdinalIgnoreCase))
      {
        using (var client = new HttpClient())
        {
          using (var response = await client.GetAsync(inputFile, HttpCompletionOption.ResponseHeadersRead))
          {
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
              using (var streamReader = new StreamReader(stream))
              {
                await Chunk(streamReader, outputDirectory, lineCount);
              }
            }
          }
        }
      }
      else
      {
        using (var reader = File.OpenText(inputFile))
        {
          await Chunk(reader, outputDirectory, lineCount);
        }
      }

      stopwatch.Stop();
      Console.WriteLine($"Chunking took: {stopwatch.Elapsed:g}");
    }

    private static async Task Chunk(StreamReader reader, string outputDirectory, int lineCount)
    {
      await reader.ReadLineAsync(); // Get rid of starting [

      var bodyWriter = new Writer(outputDirectory, "bodies", lineCount);
      var starWriter = new Writer(outputDirectory, "stars", lineCount);
      bodyWriter.Start();
      starWriter.Start();

      while (!reader.EndOfStream)
      {
        var line = await reader.ReadLineAsync();
        if (line == "]")
        {
          // Reached end of file, signal we're done.
          break;
        }

        if (line.Contains("isScoopable"))
        {
          starWriter.Add(line);
        }
        else
        {
          bodyWriter.Add(line);
        }
      }

      await bodyWriter.End();
      await starWriter.End();
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
  }
}