using System.Collections.Concurrent;
using System.Runtime.Caching;

namespace DataProcessor;

static internal class FilesToProcess
{
    public static ConcurrentDictionary<string, string> Files = new();
    //public static MemoryCache Files = MemoryCache.Default; 
}
