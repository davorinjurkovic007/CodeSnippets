using CsvHelper.Configuration;
using System.Globalization;

namespace DataProcessor;

internal class ProcessedOrderMap : ClassMap<ProcessedOrder>
{
    public ProcessedOrderMap()
    {
        //Map(po => po.OrderNumber).Name("OrderNumer");
        AutoMap(CultureInfo.InvariantCulture);
        Map(po => po.Customer).Name("CustomerNumber");
        Map(po => po.Amount).Name("Quantity");
    }
}
