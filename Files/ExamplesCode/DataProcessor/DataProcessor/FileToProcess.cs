using System.Collections.Concurrent;

namespace DataProcessor;

static internal class FileToProcess
{
    public static ConcurrentDictionary<string, string> Files = new();
}
