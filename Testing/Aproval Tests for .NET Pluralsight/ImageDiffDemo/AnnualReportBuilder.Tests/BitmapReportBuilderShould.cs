using ApprovalTests.Reporters;
using AnnualReportBuilder;
using Xunit;
using ApprovalTests;
using ApprovalTests.Reporters.TestFrameworks;
using AnnualReportBuilder.Tests;

namespace Tests
{
    public class BitmapReportBuilderShould
    {
        [Fact]
        //[UseReporter(typeof(FileLauncherReporter), typeof(DiffReporter) /*,typeof(P4MergeReporter), typeof(BeyondCompareReporter)*/, typeof(ClipboardReporter))]
        [UseReporter(typeof(OCRReporter), typeof(ClipboardReporter))]
        public void RenderPNGImage()
        {
            var model = new ReportModel
            {
                Title = "Annual Report",
                ReportLines =
                {
                    "Line 1x",
                    "Line 2",
                    "Line 3",
                    "Line 4",
                    "Line 5"
                }
            };

            var sut = new BitmapReportBuilder(model);

            byte[] bitmap = sut.Render();

            Approvals.VerifyBinaryFile(bitmap, ".png");
        }
    }
}
