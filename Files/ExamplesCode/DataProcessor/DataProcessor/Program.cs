using DataProcessor;
using System.Runtime.Caching;
using System.Runtime.InteropServices;
using static System.Console;

// NOTE: Par primjera, kada bi htjeli da to radi na Linux-u.
// Nema čistog rješenja, nego se mora iči preko posebnog paketa. 
// https://www.nuget.org/packages/microsoft.extensions.fileproviders.physical/
// https://github.com/dotnet/runtime/discussions/69700
// https://stackoverflow.com/questions/57024640/why-filesystemwatcher-doesnt-work-in-linux-container-watching-windows-volume/57025115#57025115
// https://stackoverflow.com/questions/67214771/how-to-use-filesystemwatcher-on-linux-with-net-5-0
// https://syntackle.com/blog/the-issue-of-watching-file-changes-in-docker/
// How to Build a File Watcher Service in C# Using FileSystemWatcher
// https://en.ittrip.xyz/c-sharp/csharp-file-watcher
// U slučaju kada je dokument prevelika da bi sa njima baratali u memoriju, koristiti ovo: 
// Memory-mapped files
// https://learn.microsoft.com/en-us/dotnet/standard/io/memory-mapped-files
// RandomAccess Class
// https://learn.microsoft.com/en-us/dotnet/api/system.io.randomaccess?view=net-10.0

WriteLine("Parsing command line options");

var directoryToWatch = args[0];

// Command line validation omitted for brevity

if(!Directory.Exists(directoryToWatch))
{
    WriteLine($"ERROR: {directoryToWatch} does not exist");
    WriteLine("Press enter to quit.");
    ReadLine();
    return;
}

ProcessExistingFiles(directoryToWatch);

WriteLine($"Watching directory {directoryToWatch} for changes");
using var inputFileWatcher = new FileSystemWatcher(directoryToWatch);
//using var timer = new Timer(ProcessFiles!, null, 0, 1000);

inputFileWatcher.IncludeSubdirectories = false;
inputFileWatcher.InternalBufferSize = 32_768; // 32 KB
inputFileWatcher.Filter = "*.*"; // this is the default
inputFileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;

inputFileWatcher.Created += FileCreated;
inputFileWatcher.Changed += FileChanged;
inputFileWatcher.Deleted += FileDeleted;
inputFileWatcher.Renamed += FileRenamed;
inputFileWatcher.Error += WatcherError;

inputFileWatcher.EnableRaisingEvents = true; 

WriteLine("Press enter to quit.");
ReadLine();

//var command = args[0];

//if(command == "--file")
//{What 
//    var filePath = args[1];

//    // Check if path is absolute
//    if(!Path.IsPathFullyQualified(filePath))
//    {
//        WriteLine($"ERROR: path '{filePath}' must be fully qualified.");
//        ReadLine();
//        return;
//    }

//    WriteLine($"Single file {filePath} selected");
//    ProcessSingleFile(filePath);
//}
//else if(command == "--dir")
//{
//    var directoryPath = args[1];
//    var fileType = args[2];
//    WriteLine($"Directory {directoryPath} selected for {fileType} files");
//    ProcessDirectory(directoryPath, fileType);
//}
//else
//{
//    WriteLine("Invalid command line options");
//}

//WriteLine("Press enter to quit.");
//ReadLine();

//void ProcessSingleFile(string filePath)
//{
//    var fileProcessor = new FileProcessor(filePath);
//    fileProcessor.Process();
//}

//void ProcessDirectory(string directoryPath, string fileType)
//{
//    switch(fileType)
//    {
//        case "TEXT":
//            string[] textFiles = Directory.GetFiles(directoryPath, "*.txt");
//            foreach(var textFilePath in textFiles)
//            {
//                var fileProcessor = new FileProcessor(textFilePath);
//                fileProcessor.Process();
//            }
//            break;
//        default:
//            WriteLine($"ERROR: {fileType} is not supported");
//            return;
//    }
//}

void FileCreated(object sender, FileSystemEventArgs e)
{
    WriteLine($"* File created: {e.Name} - type: {e.ChangeType}");

    //ProcessSingleFile(e.FullPath);
    //FilesToProcess.Files.TryAdd(e.FullPath, e.FullPath);
    AddToCache(e.FullPath);
}

void FileChanged(object sender, FileSystemEventArgs e)
{
    WriteLine($"* File changed. {e.Name} - type: {e.ChangeType}");

    //ProcessSingleFile(e.FullPath);
    //FilesToProcess.Files.TryAdd(e.FullPath, e.FullPath);
    AddToCache(e.FullPath);
}

void FileDeleted(object sender, FileSystemEventArgs e)
{
    WriteLine($"* File deleted: {e.Name} - type: {e.ChangeType}");
}

void FileRenamed(object sender, RenamedEventArgs e)
{
    WriteLine($"* File renamed: {e.OldName} to {e.Name} - type: {e.ChangeType}");
}

void WatcherError(object sender, ErrorEventArgs e)
{
    WriteLine($"ERROR: file system watching may no longer be active: {e.GetException()}");
}

//void ProcessFiles(object stateInfo)
//{
//    foreach(var filename in FilesToProcess.Files.Keys)
//    {
//        if(FilesToProcess.Files.TryRemove(filename, out _))
//        {
//            var fileProcessor = new FileProcessor(filename);
//            fileProcessor.Process();
//        }
//    }
//}

void AddToCache(string fullPath)
{
    var item = new CacheItem(fullPath, fullPath);

    var policy = new CacheItemPolicy
    {
        RemovedCallback = ProcessFile,
        SlidingExpiration = TimeSpan.FromSeconds(2)
    };

    FilesToProcess.Files.Add(item, policy );
}

void ProcessFile(CacheEntryRemovedArguments args)
{
    WriteLine($"* Cache item removed: {args.CacheItem.Key} because {args.RemovedReason}");

    if(args.RemovedReason == CacheEntryRemovedReason.Expired)
    {
        var fileProcessor = new FileProcessor(args.CacheItem.Key);
        fileProcessor.Process();
    }
    else
    {
        WriteLine($"WARNING: {args.CacheItem.Key} was removed unexpectedly and may... ");
    }
}

void ProcessExistingFiles(string inputDirectory)
{
    WriteLine($"Cheking {inputDirectory} for existing files");

    foreach(var filePath in Directory.EnumerateFiles(inputDirectory))
    {
        WriteLine($"  - Found {filePath}");
        AddToCache(filePath);
    }
}