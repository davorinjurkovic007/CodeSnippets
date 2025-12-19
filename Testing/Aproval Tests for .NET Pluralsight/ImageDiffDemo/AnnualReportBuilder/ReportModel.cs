using System.Collections.Generic;

namespace AnnualReportBuilder
{
    public class ReportModel
    {        
        public string Title { get; set; }

        public List<string> ReportLines { get; set; } = new List<string>();
    }
}