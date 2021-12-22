using System.Text;

namespace PersonnelManagementSystem.Employment;

public sealed class Manager : Sales
{
    public Manager(string firstName, string lastName, string id, DateTime dateOfEmployment)
        : base(firstName, lastName, id, dateOfEmployment)
    {
        BaseSalary = 7000M;
    }

    protected override string EmployeeType() => "Manager";

    /// <summary>
    /// Get monthly salary.
    /// </summary>
    /// <returns>Salary.</returns>
    public override decimal Salary() =>
        SalaryManager.Calculate(DateOfEmployment, BaseSalary, 1, 35, DownSubordinateBonusParts());

    /// <summary>
    /// Get subordinate's salary.
    /// </summary>
    /// <param name="id">Subordinate identifier.</param>
    /// <returns>Subordinate's salary.</returns>
    public override decimal? GetSubordinateSalary(string id)
    {
        foreach (var subordinate in Subordinates)
        {
            if (subordinate.Id == id)
            {
                return subordinate.Salary();
            }

            if (subordinate is not Sales sales)
            {
                continue;
            }
            
            var result = sales.GetSubordinateSalary(id);
                
            if (result is not null)
            {
                return result;
            }
        }

        return null;
    }

    /// <summary>
    /// Get salary of all subordinates.
    /// </summary>
    /// <returns>All salary.</returns>
    public override decimal GetAllSubordinateSalary()
    {
        var count = 0M;

        foreach (var subordinate in Subordinates)
        {
            count += subordinate.Salary();

            if (subordinate is Sales sales)
            {
                count += sales.GetAllSubordinateSalary();
            }
        }

        return count;
    }

    protected override decimal DownSubordinateBonusParts() =>
        Subordinates.Sum(subordinate => subordinate.Salary() / 100 * 0.3M);

    /// <summary>
    /// Add new subordinate to list.
    /// </summary>
    /// <param name="firstName">First name.</param>
    /// <param name="lastName">Last name.</param>
    /// <param name="id">Unique identifier.</param>
    /// <param name="dateOfEmployment">Date of employment.</param>
    /// <param name="type">Type of employee.</param>
    public override void Add(string firstName, string lastName, string id, DateTime dateOfEmployment, EmployeeType type)
    {
        switch (type)
        {
            case Employment.EmployeeType.Employee:
                Subordinates.Add(new Employee(firstName, lastName, id, dateOfEmployment));
                break;
            case Employment.EmployeeType.Sales:
                Subordinates.Add(new Sales(firstName, lastName, id, dateOfEmployment));
                break;
        }
    }

    /// <summary>
    /// Add new employee as subordinate in subordinates list.
    /// </summary>
    /// <param name="firstName">First name.</param>
    /// <param name="lastName">Last name.</param>
    /// <param name="id">Unique identifier.</param>
    /// <param name="dateOfEmployment">Date of employment.</param>
    /// <param name="superiorId">Superior person identifier.</param>
    /// <param name="type">Type of employee.</param>
    /// <returns>Operation status.</returns>
    public bool TryAddToInternal(string firstName, string lastName, string id, DateTime dateOfEmployment, string superiorId, EmployeeType type)
    {
        foreach (var subordinate in Subordinates)
        {
            if (subordinate is not Sales sales || subordinate.Id != superiorId)
            {
                continue;
            }
            
            sales.Add(firstName, lastName, id, dateOfEmployment, type);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Remove employee from subordinate list.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <returns>Operation status.</returns>
    public override bool TryRemove(string id)
    {
        foreach (var subordinate in Subordinates)
        {
            if (subordinate.Id == id)
            {
                Subordinates.Remove(subordinate);
                return true;
            }

            if (subordinate is not Sales sales)
            {
                continue;
            }
                
            if (sales.TryRemove(id))
            {
                return true;
            }
        }

        return false;
    }
    
    /// <summary>
    /// Remove subordinate from employee list of subordinate.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <returns>Operation status.</returns>
    public bool TryRemoveInternal(string id)
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
    public override int SubordinateCount()
    {
        var count = 0;

        foreach (var subordinate in Subordinates)
        {
            count++;

            if (subordinate is Sales sales)
            {
                count += sales.SubordinateCount();
            }
        }

        return count;
    }

    /// <summary>
    /// Get text representation of subordinate list.
    /// </summary>
    /// <param name="nestingLevel"></param>
    /// <returns>String representation.</returns>
    public override string GetSubordinateList(int nestingLevel = 0)
    {
        StringBuilder sb = new();

        foreach (var subordinate in Subordinates)
        {
            sb.Append(subordinate.GetProfileInfo(nestingLevel + 1));

            if (subordinate is Sales sales)
            {
                sb.Append(sales.GetSubordinateList(nestingLevel + 1));
            }
        }

        return sb.ToString();
    }
}
