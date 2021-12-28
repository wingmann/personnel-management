namespace Employment.Interfaces;

/// <summary>
/// First level position and base class for management.
/// </summary>
public interface IEmployee
{
    /// <summary>
    /// Person data.
    /// </summary>
    IPerson Person { get; }

    /// <summary>
    /// Unique identifier.
    /// </summary>
    string Id { get; init; }
    
    /// <summary>
    /// Date of employment.
    /// </summary>
    DateTime DateOfEmployment { get; init; }

    /// <summary>
    /// Base salary count.
    /// </summary>
    decimal BaseSalary { get; init; }

    /// <summary>
    /// Get monthly salary.
    /// </summary>
    /// <returns>Salary.</returns>
    decimal Salary();

    /// <summary>
    /// Get full information about employee with text representation.
    /// </summary>
    /// <param name="nestingLevel">Display nesting level.</param>
    /// <returns>Information about employee.</returns>
    string GetProfileInfo(int nestingLevel);
}
