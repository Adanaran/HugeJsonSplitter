using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HugeJsonSplitter
{
  public class Writer
  {
    private readonly string outputDir;
    private readonly string fileName;
    private readonly int maxLineCount;

    private readonly IList<Task> tasks;

    private int lineCount;
    private int partCount = -1;

    private BlockingCollection<string> dataToWrite;

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
      dataToWrite = new BlockingCollection<string>();
      var current = dataToWrite;
      tasks.Add(Task.Run(() => Write(current)));
    }

    public void Add(string value)
    {
      if (lineCount >= maxLineCount)
      {
        dataToWrite.Add(value.Substring(0, value.Length - 1));
        dataToWrite.CompleteAdding();
        lineCount = 0;
        Start();
      }
      else
      {
        dataToWrite.Add(value);
        lineCount++;
      }
    }

    public void Add(Element element)
    {
      var elementsToWrite = element.ToString();
      if (lineCount >= maxLineCount)
      {
        dataToWrite.Add(elementsToWrite);
        dataToWrite.CompleteAdding();
        lineCount = 0;
        Start();
      }
      else
      {
        dataToWrite.Add(elementsToWrite + ",");
        lineCount++;
      }
    }

    public async Task End()
    {
      dataToWrite.CompleteAdding();
      await Task.WhenAll(tasks);
    }

    private async void Write(BlockingCollection<string> queue)
    {
      using (var streamWriter = File.CreateText(Path.Combine(outputDir, string.Format("{0}{1}.json", fileName, partCount))))
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
  }
}