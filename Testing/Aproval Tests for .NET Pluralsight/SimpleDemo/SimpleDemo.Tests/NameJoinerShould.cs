using ApprovalTests;
using ApprovalTests.Reporters;

namespace SimpleDemo.Tests;

public class NameJoinerShould
{
    [Test]
    [UseReporter(typeof(DiffReporter))]
    public void JoinNames()
    {
        var sut = new NameJoiner();

        string result = sut.Join("Jason", "Roberts");

        // Use Approval Tests to verify ("assert") the result
        Approvals.Verify(result);
    }
}
