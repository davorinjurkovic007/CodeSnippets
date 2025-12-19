using System.Text;

namespace ReportGenerator
{
    public class HtmlReportBuilder
    {
        private readonly ReportModel _reportModel;

        public HtmlReportBuilder(ReportModel reportModel)
        {
            this._reportModel = reportModel;
        }

        public string Build()
        {
            var reportHtml = new StringBuilder();

            reportHtml.AppendLine("<HTML>");
            reportHtml.AppendLine("<BODY style='background-color: orange'>");

            AddReportIntroInfo(reportHtml);
            AddReportLines(reportHtml);

            reportHtml.AppendLine("</BODY>");
            reportHtml.AppendLine("</HTML>");

            return reportHtml.ToString();
        }

        private void AddReportLines(StringBuilder sb)
        {
            foreach(var line in _reportModel.ReportLines)
            {
                sb.AppendFormat("<p>{0}</p>", line);
                sb.AppendLine();
            }
        }

        private void AddReportIntroInfo(StringBuilder sb)
        {
            sb.AppendFormat("<H1>{0}</H1>", _reportModel.Title);
            sb.AppendLine();
        }
    }
}
