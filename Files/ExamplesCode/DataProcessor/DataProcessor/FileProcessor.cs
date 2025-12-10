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
        if (rootDirectoryPath is null)
        {
            throw new InvalidOperationException($"Cannot determine root directory path");
        }

        WriteLine($"Root data path is {rootDirectoryPath}");

        // Check if backup dir exist
        string backupDirectoryPath = Path.Combine(rootDirectoryPath, BackupDirectoryName);

        // Raditi će, i ako direktorij postoji. Ova provjera u biti niti ne treba. 
        if (!Directory.Exists(backupDirectoryPath))
        {
            WriteLine($"Creating {backupDirectoryPath}");
            Directory.CreateDirectory( backupDirectoryPath );
        }

        // Copy file to backup directory
        string inputFileName = Path.GetFileName( InputFilePath );
        string backupFilePath = Path.Combine( backupDirectoryPath, inputFileName );
        WriteLine($"Copying {InputFilePath} to {backupFilePath}");
        File.Copy(InputFilePath, backupFilePath, true);

        // Move to in progress dir
        Directory.CreateDirectory(Path.Combine(rootDirectoryPath, InProgressDirectoryName));
        string inProgressFilePath = Path.Combine(rootDirectoryPath, InProgressDirectoryName, inputFileName);

        if(File.Exists(inProgressFilePath))
        {
            WriteLine($"ERROR: a file with the name {inProgressFilePath} is already exists");
        }

        WriteLine($"Moving {InputFilePath} to {inProgressFilePath}");
        File.Move(InputFilePath, inProgressFilePath);

        // Determine type of file
        string extension = Path.GetExtension(InputFilePath);
        switch (extension)
        {
            case ".txt":
                ProcessTextFile(inProgressFilePath); 
                break;
            default:
                WriteLine($"{extension} is an unsupported file type.");
                break;
        }

        // Move file after processing is complete
        string completeDirectoryPath = Path.Combine(rootDirectoryPath, CompleteDirectoryName);
        Directory.CreateDirectory(completeDirectoryPath);

        string fileNameWithCompletedExtension = Path.ChangeExtension(inputFileName, ".complete");
        string coompletedFileName = $"{Guid.NewGuid()}_{fileNameWithCompletedExtension}";

        string completedFilePath = Path.Combine(completeDirectoryPath, coompletedFileName);

        WriteLine($"Moving {inProgressFilePath} to {completedFilePath}");
        File.Move(inProgressFilePath, completedFilePath);

        string? inProgressDirectoryPath = Path.GetDirectoryName(inProgressFilePath);
        Directory.Delete(inProgressDirectoryPath!, true);
    }

    private void ProcessTextFile(string inProgressFilePath)
    {
        WriteLine($"Processing text file {inProgressFilePath}");

        // Read in and process
    }
}
