using System.Text;
using Employment.Interfaces;

namespace Employment;

/// <summary>
/// First level position and base class for management.
/// </summary>
public class Employee : IEmployee
{
    /// <summary>
    /// Person data.
    /// </summary>
    public IPerson Person { get; }
    
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public string Id { get; init; }
    
    /// <summary>
    /// Date of employment.
    /// </summary>
    public DateTime DateOfEmployment { get; init; }
    
    /// <summary>
    /// Base salary count.
    /// </summary>
    public decimal BaseSalary { get; init; }
    
    public Employee(IPerson person, string id, DateTime dateOfEmployment)
    {
        Person = person;
        Id = id;
        BaseSalary = 3000M;
        DateOfEmployment = dateOfEmployment;
    }
    
    protected virtual string EmployeeType() => "Employee";

    /// <summary>
    /// Get monthly salary.
    /// </summary>
    /// <returns>Salary.</returns>
    public virtual decimal Salary() => SalaryManager.Calculate(DateOfEmployment, BaseSalary, 3, 30);

    /// <summary>
    /// Get full information about employee with text representation.
    /// </summary>
    /// <param name="nestingLevel">Display nesting level.</param>
    /// <returns>Information about employee.</returns>
    public string GetProfileInfo(int nestingLevel = 0)
    {
        var prefix = string.Empty;

        if (nestingLevel > 0)
        {
            for (int i = 0; i < nestingLevel; i++)
            {
                prefix += "    ";
            }
        }

        StringBuilder sb = new();
        sb.AppendLine($"{prefix} -----");
        sb.AppendLine($"{prefix}| Name: {Person}");
        sb.AppendLine($"{prefix}| Position: {EmployeeType()}");
        sb.AppendLine($"{prefix}| ID: {Id}");

        return sb.ToString();
    }
}
