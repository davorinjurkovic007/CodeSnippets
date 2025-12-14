using System.IO.Abstractions;

namespace DataProcessor;

public class TextFileProcessor
{
    private readonly IFileSystem _fileSystem;

    public string InputFilePath { get; }

    public string OutputFilePath { get; }

    public TextFileProcessor(string inputFilePath, 
                             string outputFilePath,
                             IFileSystem fileSystem)
    {
        InputFilePath = inputFilePath;
        OutputFilePath = outputFilePath;
        _fileSystem = fileSystem;
    }

    public TextFileProcessor(string inputFilePath, string outputFilePath)
        : this(inputFilePath, outputFilePath, new FileSystem())
    {
        
    }

    //public void Process()
    //{
    //    // Using read all text
    //    //string originalText = File.ReadAllText(InputFilePath);
    //    //string processedText = originalText.ToUpperInvariant();
    //    //File.WriteAllText(OutputFilePath, processedText);

    //    // Using read all lines
    //    try
    //    {
    //        string[] lines = File.ReadAllLines(InputFilePath);
    //        lines[1] = lines[1].ToUpperInvariant(); // Assumes there is a line 2 in the file. 
    //        File.WriteAllLines(OutputFilePath, lines);
    //    }
    //    catch(IOException ex)
    //    {
    //        // Log / retry
    //        Console.WriteLine(ex);
    //        throw;
    //    }
    //}

    //public void Process()
    //{
    //    //var openToReadFrom = new FileStreamOptions { Mode = FileMode.Open };
    //    //using var inputFileStream = new FileStream(InputFilePath, openToReadFrom);
    //    //using var inputStreamReader = new StreamReader(inputFileStream);

    //    using StreamReader inputStreamReader = File.OpenText(InputFilePath);

    //    //var createToWriteTo = new FileStreamOptions 
    //    //{ 
    //    //    Mode = FileMode.CreateNew,
    //    //    Access = FileAccess.Write,
    //    //};
    //    //using var outputFileStream = new FileStream(OutputFilePath, createToWriteTo);
    //    //using var outputStreamWriter = new StreamWriter(outputFileStream);

    //    using var outputStreamWriter = new StreamWriter(OutputFilePath);

    //    while(!inputStreamReader.EndOfStream)
    //    {
    //        string inputLine = inputStreamReader.ReadLine()!;
    //        string processedLine = inputLine.ToUpperInvariant();

    //        bool isLastLine = inputStreamReader.EndOfStream;

    //        if (isLastLine)
    //        {
    //            outputStreamWriter.Write(processedLine);
    //        }
    //        else
    //        {
    //            outputStreamWriter.WriteLine(processedLine);
    //        }
    //    }
    //}

    //public void Process()
    //{
    //    using StreamReader inputStreamReader = File.OpenText(InputFilePath);
    //    using var outputStreamWriter = new StreamWriter(OutputFilePath);

    //    var currentLineNumber = 1;
    //    while (!inputStreamReader.EndOfStream)
    //    {
    //        string inputLine = inputStreamReader.ReadLine()!;

    //        if (currentLineNumber == 2)
    //        {
    //            inputLine = inputLine.ToUpperInvariant();
    //        }

    //        bool isLastLine = inputStreamReader.EndOfStream;

    //        if (isLastLine)
    //        {
    //            outputStreamWriter.Write(inputLine);
    //        }
    //        else
    //        {
    //            outputStreamWriter.WriteLine(inputLine);
    //        }

    //        currentLineNumber++;
    //    }
    //}

    public void Process()
    {
        using var inputStreamReader = _fileSystem.File.OpenText(InputFilePath);
        using var outputStreamWriter = _fileSystem.File.CreateText(OutputFilePath);

        var currentLineNumber = 1;
        while (!inputStreamReader.EndOfStream)
        {
            string inputLine = inputStreamReader.ReadLine()!;

            if (currentLineNumber == 2)
            {
                inputLine = inputLine.ToUpperInvariant();
            }

            bool isLastLine = inputStreamReader.EndOfStream;

            if (isLastLine)
            {
                outputStreamWriter.Write(inputLine);
            }
            else
            {
                outputStreamWriter.WriteLine(inputLine);
            }

            currentLineNumber++;
        }
    }
}
