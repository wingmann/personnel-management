namespace PersonnelManagementSystem.Employment.Interfaces;

/// <summary>
/// Management representation.
/// </summary>
public interface IManagement
{
    /// <summary>
    /// Get subordinate's salary.
    /// </summary>
    /// <param name="id">Subordinate identifier.</param>
    /// <returns>Subordinate's salary.</returns>
    decimal? GetSubordinateSalary(string id);

    /// <summary>
    /// Get salary of all subordinates.
    /// </summary>
    /// <returns>All salary.</returns>
    decimal GetAllSubordinateSalary();

    /// <summary>
    /// Add new subordinate to list.
    /// </summary>
    /// <param name="firstName">First name.</param>
    /// <param name="lastName">Last name.</param>
    /// <param name="id">Unique identifier.</param>
    /// <param name="dateOfEmployment">Date of employment.</param>
    /// <param name="type">Type of employee.</param>
    void Add(string firstName, string lastName, string id, DateTime dateOfEmployment, EmployeeType type);

    /// <summary>
    /// Remove employee from subordinate list.
    /// </summary>
    /// <param name="id">Unique identifier.</param>
    /// <returns>Operation status.</returns>
    bool TryRemove(string id);
    
    /// <summary>
    /// Get count of subordinate.
    /// </summary>
    /// <returns>Count of subordinate.</returns>
    int SubordinateCount();

    /// <summary>
    /// Get text representation of subordinate list.
    /// </summary>
    /// <param name="nestingLevel"></param>
    /// <returns>String representation.</returns>
    string GetSubordinateList(int nestingLevel);
}
