using DataProcessor;
using System.Runtime.InteropServices;
using static System.Console;

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

WriteLine($"Watching directory {directoryToWatch} for changes");
using var inputFileWatcher = new FileSystemWatcher(directoryToWatch);
using var timer = new Timer(ProcessFiles!, null, 0, 1000);

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
    FileToProcess.Files.TryAdd(e.FullPath, e.FullPath);
}

void FileChanged(object sender, FileSystemEventArgs e)
{
    WriteLine($"* File changed. {e.Name} - type: {e.ChangeType}");

    //ProcessSingleFile(e.FullPath);
    FileToProcess.Files.TryAdd(e.FullPath, e.FullPath);
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

void ProcessFiles(object stateInfo)
{
    foreach(var filename in FileToProcess.Files.Keys)
    {
        if(FileToProcess.Files.TryRemove(filename, out _))
        {
            var fileProcessor = new FileProcessor(filename);
            fileProcessor.Process();
        }
    }
}
