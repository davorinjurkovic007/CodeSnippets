using ApprovalTests.Maintenance;
using Xunit;

namespace ReportGenerator.Tests
{
    public class ApprovalTestsMaintenance
    {
        [Fact]
        public void Maintenance()
        {
            ApprovalMaintenance.VerifyNoAbandonedFiles();
        }
    }
}
