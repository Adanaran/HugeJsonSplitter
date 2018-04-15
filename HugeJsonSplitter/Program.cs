using System;
using System.Collections.Concurrent;
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
    private const string PartName = "chunk{0}.json";

    public static async Task Main(string[] args)
    {
      var arguments = args.ToList();
      var lineCount = GetLineCount(arguments);

      var inputFile = EnsureInputFile(arguments);
      if (inputFile == null)
      {
        return;
      }

      var outputDirectory = EnsureOutputDirectory(arguments);

      var queue = new BlockingCollection<string>();

      var tasks = new List<Task>();
      var stopwatch = Stopwatch.StartNew();

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
                await Chunk(streamReader, queue, tasks, outputDirectory, lineCount);
              }
            }
          }
        }
      }
      else
      {
        using (var reader = File.OpenText(inputFile))
        {
          await Chunk(reader, queue, tasks, outputDirectory, lineCount);
        }
      }

      await Task.WhenAll(tasks);
      stopwatch.Stop();

      Console.WriteLine($"Chunking took: {stopwatch.Elapsed:g}");
    }

    private static async Task Chunk(StreamReader reader, BlockingCollection<string> queue, List<Task> tasks, string outputDirectory, int lineCount)
    {
      var fileCount = 0;
      var count = 0;
      await reader.ReadLineAsync(); // Get rid of starting [
      while (!reader.EndOfStream)
      {
        var line = await reader.ReadLineAsync();
        if (line == "]")
        {
          queue.CompleteAdding();
          return;
        }

        if (count == 0)
        {
          var nextFileNumber = fileCount;
          var nextFileQueue = queue;
          tasks.Add(Task.Run(() => Write(nextFileQueue, outputDirectory, nextFileNumber)));
        }

        if (count >= lineCount)
        {
          if (line.EndsWith(','))
          {
            line = line.Substring(0, line.Length - 1);
          }

          queue.Add(line);
          queue.CompleteAdding();

          count = 0;
          fileCount++;
          queue = new BlockingCollection<string>();
        }
        else
        {
          queue.Add(line);
          count++;
        }
      }
    }

    private static async void Write(BlockingCollection<string> queue, string outputDir, int number)
    {
      using (var streamWriter = File.CreateText(Path.Combine(outputDir, string.Format(PartName, number))))
      {
        await streamWriter.WriteLineAsync("[");
        while (!queue.IsCompleted)
        {
          if (queue.TryTake(out var line))
          {
            await streamWriter.WriteLineAsync(line);
          }
        }

        await streamWriter.WriteLineAsync("]");
      }
    }

    private static int GetLineCount(List<string> arguments)
    {
      var linecountIndex = arguments.IndexOf("--linecount");
      if (linecountIndex == -1 || !int.TryParse(arguments[linecountIndex + 1], out var lineCount))
      {
        lineCount = 1_000_000;
      }

      return lineCount;
    }

    private static string EnsureInputFile(List<string> arguments)
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

    private static string EnsureOutputDirectory(List<string> arguments)
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