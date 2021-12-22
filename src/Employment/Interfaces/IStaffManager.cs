namespace PersonnelManagementSystem.Employment.Interfaces;

/// <summary>
/// Employment manager in company.
/// </summary>
public interface IStaffManager
{
    /// <summary>
    /// Add new employee.
    /// </summary>
    /// <param name="firstName">First name.</param>
    /// <param name="lastName">Last name.</param>
    /// <param name="id">Unique identifier.</param>
    /// <param name="dateOfEmployment">Date of employment.</param>
    /// <param name="type">Type of employee.</param>
    void Add(string firstName, string lastName, string id, DateTime dateOfEmployment, EmployeeType type);

    /// <summary>
    /// Add subordinate to management position.
    /// </summary>
    /// <param name="firstName">First name.</param>
    /// <param name="lastName">Last name.</param>
    /// <param name="superiorId">Superior person identifier.</param>
    /// <param name="id">Unique identifier.</param>
    /// <param name="dateOfEmployment">Date of employment.</param>
    /// <param name="type">Type of new employee.</param>
    /// <returns>Operation status.</returns>
    bool TryAddSubordinate(string firstName, string lastName, string id, DateTime dateOfEmployment, string superiorId, EmployeeType type);

    /// <summary>
    /// Try remove employee. Search in all subordinate lists.
    /// </summary>
    /// <param name="id">Unique removable identifier.</param>
    /// <returns>Operation status.</returns>
    bool TryRemove(string id);
    
    /// <summary>
    /// Total employee count with all subordinates.
    /// </summary>
    /// <returns>Count of employees.</returns>
    int EmployeeCount();

    /// <summary>
    /// Get subordinate's salary.
    /// </summary>
    /// <param name="id">Subordinate identifier.</param>
    /// <returns>Subordinate's salary.</returns>
    decimal? GetSalary(string id);

    /// <summary>
    /// Get salary of all company employees.
    /// </summary>
    /// <returns>All salary.</returns>
    decimal GetAllSalary();

    /// <summary>
    /// Get text representation of subordinate list.
    /// </summary>
    /// <param name="nestingLevel">Display nesting level.</param>
    /// <returns>String representation.</returns>
    string GetEmployeeList(int nestingLevel = 0);
}
