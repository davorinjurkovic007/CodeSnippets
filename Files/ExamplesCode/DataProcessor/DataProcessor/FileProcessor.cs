using static System.Console;

namespace DataProcessor;

internal class FileProcessor
{
    private const string BackupDirectoryName = "backup";
    private const string InProgressDirectoryName = "processing";
    private const string CompleteDirectoryName = "complete";

    public string InputFilePath { get; }

    public FileProcessor(string filePath) => InputFilePath = filePath; 

    public void Process()
    {
        WriteLine($"Begin process of {InputFilePath}");
        
        // Check if file exists
        if(!File.Exists( InputFilePath ))
        {
            WriteLine($"ERROR: file {InputFilePath} does not exist.");
            return;
        }

        //string? rootDirectoryPath = new DirectoryInfo( InputFilePath ).Parent?.FullName;
        string? rootDirectoryPath = new DirectoryInfo( InputFilePath ).Parent?.Parent?.FullName;

        WriteLine($"Root data path is {rootDirectoryPath}");
    }
}
