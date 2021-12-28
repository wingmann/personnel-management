namespace Employment.Interfaces;

/// <summary>
/// Person's data representation.
/// </summary>
public interface IPerson
{
    /// <summary>
    /// First name.
    /// </summary>
    string FirstName { get; }

    /// <summary>
    /// Last name.
    /// </summary>
    string LastName { get; }

    /// <summary>
    /// Text name representation.
    /// </summary>
    string DisplayName { get; }
}
