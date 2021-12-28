using Employment.Interfaces;

namespace Employment;

/// <summary>
/// Person's data representation.
/// </summary>
public class Person : IPerson
{
    /// <summary>
    /// First name.
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    /// Last name.
    /// </summary>
    public string LastName { get; }

    /// <summary>
    /// Full name.
    /// </summary>
    public string DisplayName => string.Format($"{FirstName} {LastName}");

    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public override string ToString() => DisplayName;
}
