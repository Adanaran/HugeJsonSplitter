using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HugeJsonSplitter
{
  public class Writer<TElement>
    where TElement : JsonObjectBase
  {
    private readonly string outputDir;
    private readonly string fileName;
    private readonly int maxLineCount;

    private readonly IList<Task> tasks;

    private int lineCount;
    private int partCount = -1;

    private BlockingCollection<TElement> dataToWrite;

    public Writer(string outputDir, string fileName, int maxLineCount)
    {
      this.outputDir = outputDir;
      this.fileName = fileName;
      this.maxLineCount = maxLineCount;
      tasks = new List<Task>();
    }

    public void Start()
    {
      partCount++;
      dataToWrite = new BlockingCollection<TElement>();
      var current = dataToWrite;
      tasks.Add(Task.Run(() => Write(current)));
    }

    public void Add(TElement element)
    {
      dataToWrite.Add(element);
      if (lineCount >= maxLineCount)
      {
        dataToWrite.CompleteAdding();
        lineCount = 0;
        Start();
      }
      else
      {
        lineCount++;
      }
    }

    public async Task End()
    {
      dataToWrite.CompleteAdding();
      await Task.WhenAll(tasks);
    }

    private async void Write(BlockingCollection<TElement> queue)
    {
      using (var streamWriter = File.CreateText(Path.Combine(outputDir, $"{fileName}{partCount}.json")))
      {
        using (var jsonTextWriter = new JsonTextWriter(streamWriter))
        {
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
    }
  }
}