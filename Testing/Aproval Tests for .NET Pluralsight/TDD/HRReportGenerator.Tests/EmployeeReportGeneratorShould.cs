using ApprovalTests;
using ApprovalTests.Reporters;

namespace HRReportGenerator.Tests
{
    public class EmployeeReportGeneratorShould
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void GenerateCorrectReportWhenMultipleEmployees()
        {
            var sut = new EmployeeReportGenerator();

            var employees = new[]
            {
                new Employee {FirstName = "Sarah", LastName = "Smith", PayGrade=1, DateOfBirth=new DateTime(1980,4,10) },
                new Employee {FirstName = "Gentry", LastName = "Smith", PayGrade=3, DateOfBirth=new DateTime(1970,7,1) },
                new Employee {FirstName = "Arnold", LastName = "Jones", PayGrade=2, DateOfBirth=new DateTime(1972,12,21) },
            };

            string reportText = sut.Generate(employees);

            Approvals.Verify(reportText);
        }
    }
}
