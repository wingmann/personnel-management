using System.Text;
using PersonnelManagementSystem.Employment.Interfaces;

namespace PersonnelManagementSystem.Employment;

public class Sales : Employee, IManagement
{
    internal readonly IList<Employee> Subordinates;

    public Sales(string firstName, string lastName, string id, DateTime dateOfEmployment)
        : base(firstName, lastName, id, dateOfEmployment)
    {
        BaseSalary = 4500M;
        Subordinates = new List<Employee>();
    }
    
    protected override string EmployeeType() => "Sales";

    /// <summary>
    /// Get monthly salary.
    /// </summary>
    /// <returns>Salary.</returns>
    public override decimal Salary() =>
        SalaryManager.Calculate(DateOfEmployment, BaseSalary, 5, 40, DownSubordinateBonusParts());

    /// <summary>
    /// Get subordinate's salary.
    /// </summary>
    /// <param name="id">Subordinate identifier.</param>
    /// <returns>Subordinate's salary.</returns>
    public virtual decimal? GetSubordinateSalary(string id)
    {
        foreach (var subordinate in Subordinates)
        {
            if (subordinate.Id == id)
            {
                return subordinate.Salary();
            }
        }

        return null;
    }

    /// <summary>
    /// Get salary of all subordinates.
    /// </summary>
    /// <returns>All salary.</returns>
    public virtual decimal GetAllSubordinateSalary() => Subordinates.Sum(subordinate => subordinate.Salary());

    protected virtual decimal DownSubordinateBonusParts() =>
        Subordinates.Sum(subordinate => subordinate.Salary() / 100M * 0.5M);

    /// <summary>
    /// Add new subordinate to list.
    /// </summary>
    /// <param name="firstName">First name.</param>
    /// <param name="lastName">Last name.</param>
    /// <param name="id">Unique identifier.</param>
    /// <param name="dateOfEmployment">Date of employment.</param>
    /// <param name="type">Type of employee.</param>
    public virtual void Add(string firstName, string lastName, string id, DateTime dateOfEmployment, EmployeeType type)
    {
        if (type is Employment.EmployeeType.Employee)
        {
            Subordinates.Add(new Employee(firstName, lastName, id, dateOfEmployment));
        }
    }

    /// <summary>
    /// Remove employee from subordinate list.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <returns>Operation status.</returns>
    public virtual bool TryRemove(string id)
    {
        foreach (var subordinate in Subordinates)
        {
            if (subordinate.Id != id)
            {
                continue;
            }
            
            Subordinates.Remove(subordinate);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Get count of subordinate.
    /// </summary>
    /// <returns>Count of subordinate.</returns>
    public virtual int SubordinateCount() => Subordinates.Count;

    /// <summary>
    /// Get text representation of subordinate list.
    /// </summary>
    /// <param name="nestingLevel"></param>
    /// <returns>String representation.</returns>
    public virtual string GetSubordinateList(int nestingLevel = 0)
    {
        StringBuilder sb = new();

        foreach (var employee in Subordinates)
        {
            sb.Append(employee.GetProfileInfo(nestingLevel + 1));
        }

        return sb.ToString();
    }
}
