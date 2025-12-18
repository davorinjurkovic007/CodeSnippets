using System.Text;

namespace HRReportGenerator
{
    public class EmployeeReportGenerator
    {
        public string Generate(Employee[] employees)
        {
            var reportText = new StringBuilder();

            reportText.AppendLine("HR Employees Report");
            reportText.AppendLine("-------------------");
            reportText.AppendLine();

            reportText.AppendLine("First Name               Last Name                  Pay Grade     DOB");
            foreach (var employee in employees)
            {
                reportText.AppendLine($"{employee.FirstName,-25}{employee.LastName,-27}{employee.PayGrade,-14}{employee.DateOfBirth:MMMM d, yyyy}");
            }

            reportText.AppendLine();
            reportText.Append($"Total Employees: {employees.Count()}");

            return reportText.ToString();
        }
    }
}
