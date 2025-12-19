using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.TestFrameworks;

namespace ReportGenerator.Tests
{
    public class HtmlReportBuilderShould
    {
        [Fact]
        // To ne radi u .NET-u
        //[UseReporter(typeof(FileLauncherReporter))]
        //[UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
        //[UseReporter(typeof(XUnit2Reporter))]
        [UseReporter(typeof(FrameworkAssertReporter))]
        public void Build()
        {
            var model = new ReportModel
            {
                Title = "Annual Report",
                ReportLines =
                {
                    "Line 1xyz",
                    "Line 2",
                    "Line 3",
                    "Line 4",
                    "Line 5"
                }
            };

            var sut = new HtmlReportBuilder(model);

            string html = sut.Build();

            // Approvals.Verify(html);
            Approvals.VerifyHtml(html);
        }
    }
}
