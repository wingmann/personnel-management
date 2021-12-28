using System.Text;
using Employment.Interfaces;

namespace Employment;

/// <summary>
/// Employment manager in company.
/// </summary>
public sealed class StaffManager : IStaffManager
{
    /// <summary>
    /// List of company employees.
    /// </summary>
    internal readonly IList<Employee> Employees;
    
    public StaffManager()
    {
        Employees = new List<Employee>();
    }

    /// <summary>
    /// Add new employee.
    /// </summary>
    /// <param name="person">Full name.</param>
    /// <param name="id">Unique identifier.</param>
    /// <param name="dateOfEmployment">Date of employment.</param>
    /// <param name="type">Type of employee.</param>
    public void Add(Person person, string id, DateTime dateOfEmployment, EmployeeType type)
    {
        switch (type)
        {
            case EmployeeType.Employee:
                Employees.Add(new Employee(person, id, dateOfEmployment));
                break;
            case EmployeeType.Sales:
                Employees.Add(new Sales(person, id, dateOfEmployment));
                break;
            case EmployeeType.Manager:
                Employees.Add(new Manager(person, id, dateOfEmployment));
                break;
        }
    }

    /// <summary>
    /// Add subordinate to management position.
    /// </summary>
    /// <param name="person">Full name.</param>
    /// <param name="superiorId">Superior person identifier.</param>
    /// <param name="id">Unique identifier.</param>
    /// <param name="dateOfEmployment">Date of employment.</param>
    /// <param name="type">Type of new employee.</param>
    /// <returns>Operation status.</returns>
    public bool TryAddSubordinate(Person person, string id, DateTime dateOfEmployment, string superiorId, EmployeeType type)
    {
        foreach (var employee in Employees)
        {
            switch (employee)
            {
                case Sales sales when employee.Id == superiorId && type is EmployeeType.Employee:
                    sales.Add(person, id, dateOfEmployment, type);
                    return true;
                case Manager manager when employee.Id == superiorId:
                {
                    switch (type)
                    {
                        case EmployeeType.Employee:
                            manager.Add(person, id, dateOfEmployment, type);
                            return true;
                        case EmployeeType.Sales:
                            manager.Add(person, id, dateOfEmployment, type);
                            return true;
                    }

                    break;
                }
                case Manager manager when manager.TryAddToInternal(person, id, dateOfEmployment, superiorId, type):
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Try remove employee. Search in all subordinate lists.
    /// </summary>
    /// <param name="id">Unique removable identifier.</param>
    /// <returns>Operation status.</returns>
    public bool TryRemove(string id)
    {
        foreach (var employee in Employees)
        {
            if (employee.Id == id)
            {
                Employees.Remove(employee);
                return true;
            }
            
            if (employee is Sales sales)
            {
                if (sales.TryRemove(id))
                {
                    return true;
                }
            }
            else if (employee is Manager manager)
            {
                if (manager.TryRemoveInternal(id))
                {
                    return true;
                }
            }
        }

        return false;
    }
    
    /// <summary>
    /// Total employee count with all subordinates.
    /// </summary>
    /// <returns>Count of employees.</returns>
    public int EmployeeCount()
    {
        var count = 0;

        foreach (var employee in Employees)
        {
            count++;

            if (employee is Sales sales)
            {
                count += sales.SubordinateCount();
            }
            else if (employee is Manager manager)
            {
                count += manager.SubordinateCount();
            }
        }

        return count;
    }

    /// <summary>
    /// Get subordinate's salary.
    /// </summary>
    /// <param name="id">Subordinate identifier.</param>
    /// <returns>Subordinate's salary.</returns>
    public decimal? GetSalary(string id)
    {
        foreach (var employee in Employees)
        {
            if (employee.Id == id)
            {
                return employee.Salary();
            }

            if (employee is Sales sales)
            {
                var result = sales.GetSubordinateSalary(id);

                if (result is not null)
                {
                    return result;
                }
            }
            else if (employee is Manager manager)
            {
                var result = manager.GetSubordinateSalary(id);

                if (result is not null)
                {
                    return result;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Get salary of all company employees.
    /// </summary>
    /// <returns>All salary.</returns>
    public decimal GetAllSalary()
    {
        var count = 0M; 

        foreach (var employee in Employees)
        {
            count += employee.Salary();
            
            if (employee is Sales sales)
            {
                count += sales.GetAllSubordinateSalary();
            }
            else if (employee is Manager manager)
            {
                count += manager.GetAllSubordinateSalary();
            }
        }

        return count;
    }

    /// <summary>
    /// Get text representation of subordinate list.
    /// </summary>
    /// <param name="nestingLevel">Display nesting level.</param>
    /// <returns>String representation.</returns>
    public string GetEmployeeList(int nestingLevel = 0)
    {
        StringBuilder sb = new();

        sb.AppendLine();

        foreach (var employee in Employees)
        {
            sb.Append(employee.GetProfileInfo());

            if (employee is Sales sales)
            {
                sb.Append(sales.GetSubordinateList());
            }
            else if (employee is Manager manager)
            {
                sb.Append(manager.GetSubordinateList());
            }
        }

        return (sb.Length > 2) ? sb.ToString() : "Empty";
    }
}
