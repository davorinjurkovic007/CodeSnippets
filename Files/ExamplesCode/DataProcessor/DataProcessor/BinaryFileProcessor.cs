namespace DataProcessor;

internal class BinaryFileProcessor
{
    public string InputFilePath { get; }

    public string OutputFilePath { get; }

    public BinaryFileProcessor(string inputFilePath, string outputFilePath)
    {
        InputFilePath = inputFilePath;
        OutputFilePath = outputFilePath;
    }

    //public void Process()
    //{
    //    byte[] data = File.ReadAllBytes(InputFilePath);

    //    byte largest = data.Max();

    //    byte[] newData = new byte[data.Length + 1];

    //    Array.Copy(data, newData, data.Length);

    //    //newData[newData.Length - 1] = largest;

    //    // feature first introduced in C# 8
    //    // -> index the last element in the array
    //    newData[^1] = largest;

    //    File.WriteAllBytes(OutputFilePath, newData);
    //}

    public void Process()
    {
        var openToReadFrom = new FileStreamOptions { Mode = FileMode.Open, };
        using FileStream inputFileStream = File.Open(InputFilePath, openToReadFrom);

        using FileStream outputFileStream = File.Create(OutputFilePath);

        const int entOfStream = -1;

        int largestByte = 0;

        // read next byte (as na int): returns -1 if end of stream
        int currentByte = inputFileStream.ReadByte();
        while(currentByte != entOfStream)
        {
            outputFileStream.WriteByte((byte)currentByte);

            if(currentByte > largestByte)
            {
                largestByte = currentByte;
            }

            currentByte = inputFileStream.ReadByte();
        }

        outputFileStream.WriteByte((byte)largestByte);
    }
}
