using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO.Abstractions;

namespace DataProcessor;

internal class CsvFileProcessor
{
    private readonly IFileSystem _fileSystem;

    public string InputFilePath { get; }
    public string OutputFilePath { get; }

    public CsvFileProcessor(string inputFilePath, 
                            string outputFilePath,
                            IFileSystem fileSystem)
    {
        InputFilePath = inputFilePath;
        OutputFilePath = outputFilePath;
        _fileSystem = fileSystem;
    }

    public CsvFileProcessor(string inputFilePath, string outputFilePath)
        :this(inputFilePath, outputFilePath, new FileSystem())
    {
        
    }

    //public void Process()
    //{
    //    using StreamReader inputReader = File.OpenText(InputFilePath);

    //    var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
    //    {
    //        Comment = '@',
    //        AllowComments = true,
    //        TrimOptions = TrimOptions.Trim,
    //        //IgnoreBlankLines = true, // this is the default
    //        //HasHeaderRecord = true, // this is the default
    //        //Delimiter = ",", // this is the default
    //        ////HeaderValidated = null, // DANGER!!! Removing data Header validation
    //        ////MissingFieldFound = null // DANGER!!! Use very caferuli. 

    //    };
    //    using CsvReader csvReader = new CsvReader(inputReader, csvConfiguration);
    //    csvReader.Context.RegisterClassMap<ProcessedOrderMap>();

    //    //IEnumerable<dynamic> records = csvReader.GetRecords<dynamic>();
    //    //IEnumerable<Order> records = csvReader.GetRecords<Order>();
    //    IEnumerable<ProcessedOrder> records = csvReader.GetRecords<ProcessedOrder>();

    //    //foreach (ProcessedOrder processedOrder in records)
    //    //{
    //    //    // With header
    //    //    //Console.WriteLine(record.OrderNumber);
    //    //    //Console.WriteLine(record.CustomerNumber);
    //    //    //Console.WriteLine(record.Description);
    //    //    //Console.WriteLine(record.Quantity);

    //    //    // Without header
    //    //    //Console.WriteLine(record.Field1);
    //    //    //Console.WriteLine(record.Field2);
    //    //    //Console.WriteLine(record.Field3);
    //    //    //Console.WriteLine(record.Field4);

    //    //    //Console.WriteLine($"Order Number: {order.OrderNumber}");
    //    //    //Console.WriteLine($"Customer Number: {order.CustomerNumber}");
    //    //    //Console.WriteLine($"Descrition; {order.Description}");
    //    //    //Console.WriteLine($"Quantity: {order.Quantity}");

    //    //    Console.WriteLine($"Order Number: {processedOrder.OrderNumber}");
    //    //    Console.WriteLine($"Customer; {processedOrder.Customer}");
    //    //    Console.WriteLine($"Amount: {processedOrder.Amount}");
    //    //}

    //    using StreamWriter output = File.CreateText(OutputFilePath);
    //    using var csvWriter = new CsvWriter(output, CultureInfo.InvariantCulture);

    //    csvWriter.WriteRecords(records);
    //}

    public void Process()
    {
        using var inputReader = _fileSystem.File.OpenText(InputFilePath);

        var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Comment = '@',
            AllowComments = true,
            TrimOptions = TrimOptions.Trim,
        };
        using CsvReader csvReader = new CsvReader(inputReader, csvConfiguration);
        csvReader.Context.RegisterClassMap<ProcessedOrderMap>();

        IEnumerable<ProcessedOrder> records = csvReader.GetRecords<ProcessedOrder>();

        using var output = _fileSystem.File.CreateText(OutputFilePath);
        using var csvWriter = new CsvWriter(output, CultureInfo.InvariantCulture);

        csvWriter.WriteRecords(records);
    }
}
