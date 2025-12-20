using ApprovalTests.Reporters;
using Xunit;
using ApprovalTests;
using ApprovalTests.Maintenance;

namespace ReportGenerator.Tests
{
    public class HtmlReportBuilderShould
    {
        [Fact]
        [UseReporter(typeof(FileLauncherReporter),typeof(DiffReporter), typeof(ClipboardReporter))]
        public void Build()
        {
            var model = new ReportModel
            {
                Title = "Annual Report",
                ReportLines =
                            {
                                "Line 1",
                                "Line 2",
                                "Line 3",
                                "Line 4",
                                "Line 5"
                            }
            };

            var sut = new HtmlReportBuilder(model);

            string html = sut.Build();

            Approvals.VerifyHtml(html);
        }
    }
}
