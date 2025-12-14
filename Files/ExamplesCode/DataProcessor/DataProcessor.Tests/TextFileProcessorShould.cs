using System.IO.Abstractions.TestingHelpers;

namespace DataProcessor.Tests;

public class TextFileProcessorShould
{
    [Fact]
    public void MakeSecondLineUpperCase()
    {
        // Create a mock input file
        var mockInputFile = new MockFileData("Line 1\nLine 2\nLine 3");

        // Setup mock file system starting state
        var mockFileSystem = new MockFileSystem();
        // NOTE: path is in memory. It is not real path on computer
        mockFileSystem.AddFile(@"c:\root\in\myfile.txt", mockInputFile);
        mockFileSystem.AddDirectory(@"c:\root\out");

        // Create TextFileProcessor with mock file system
        var sut = new TextFileProcessor(@"c:\root\in\myfile.txt", (@"c:\root\out\myfile.txt"), mockFileSystem);

        // Process test file
        // NOTE:    When we call this method, it's going to read and write data to the mock file system and not the 
        //          physical file system.
        sut.Process();

        // Check mock file system for output file
        Assert.True(mockFileSystem.FileExists(@"c:\root\out\myfile.txt"));

        // Check content of output file in mock file system
        var processedFile = mockFileSystem.GetFile(@"c:\root\out\myfile.txt");

        string[] lines = processedFile.TextContents.Split(Environment.NewLine);

        Assert.Equal("Line 1", lines[0]);
        Assert.Equal("LINE 2", lines[1]);
        Assert.Equal("Line 3", lines[2]);
    }
}
