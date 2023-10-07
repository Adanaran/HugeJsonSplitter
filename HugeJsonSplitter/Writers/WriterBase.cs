using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using HugeJsonSplitter.Models.SystemsApi;

namespace HugeJsonSplitter.Writers;

public abstract class WriterBase<TElement>
  where TElement : SystemsApiModelBase
{
  private readonly int maxLineCount;
  private readonly IList<Task> tasks;
  protected string outputDir;
  protected string fileName;
  protected int partCount = -1;
  private int lineCount;
  private BlockingCollection<TElement> dataToWrite;

  protected WriterBase(string outputDir, string fileName, int maxLineCount)
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
    tasks.Add(Task.Run((Action)(() => Write(current))));
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

  protected abstract Task Write(BlockingCollection<TElement> queue);
}