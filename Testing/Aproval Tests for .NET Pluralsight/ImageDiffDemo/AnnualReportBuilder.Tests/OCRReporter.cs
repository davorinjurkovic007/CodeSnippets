using ApprovalTests.Core;
using System.Collections.Generic;
using Xunit;

namespace AnnualReportBuilder.Tests
{
    public class OCRReporter : IApprovalFailureReporter
    {
        public void Report(string approved, string received)
        {
            var approveImageTextContents = RecognizeApprovedText(approved);
            var receivedImageTextContents = RecognizeReceivedText(received);

            // "Report" results somehow
            Assert.Equal(approveImageTextContents, receivedImageTextContents);
        }

        private List<string> RecognizeApprovedText(string approved)
        {
            // Simulate use of 3rd party OCR component
            return new List<string>
            {
                "Annual Report",
                "Line 1",
                "Line 2",
                "Line 3",
                "Line 4",
                "Line 5"
            };
        }

        private List<string> RecognizeReceivedText(string approved)
        {
            // Simulate use of 3rd party OCR component
            return new List<string>
            {
                "Annual Report",
                "Line 1x",
                "Line 2",
                "Line 3",
                "Line 4",
                "Line 5"
            };
        }
    }
}
