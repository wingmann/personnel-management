using System.Text;

namespace CommandHandler;

/// <summary>
/// General information messages.
/// </summary>
public static class Essentials
{
    /// <summary>
    /// Command list.
    /// </summary>
    /// <returns>List of possible commands.</returns>
    public static string CommandList()
    {
        StringBuilder sb = new();
        sb.AppendLine("\nCommand list -----------------------------------------------------------------");
        sb.AppendLine("| help                              Command list");
        sb.AppendLine("| count                             Get employee count");
        sb.AppendLine("| salary                            Get employee's salary");
        sb.AppendLine("| salary all                        Get all employee's salary");
        sb.AppendLine("| show info                         Show information about company");
        sb.AppendLine("| show all                          Show list of employee");
        sb.AppendLine("| add employee|sales|manager        Add new employee");
        sb.AppendLine("| add sub employee|-sales           Add subordinate to superior person");
        sb.AppendLine("| remove                            Remove employee");
        sb.AppendLine("| clear                             Console clear");
        sb.AppendLine(" -----------------------------------------------------------------------------");

        return sb.ToString();
    }
    
    /// <summary>
    /// Error message.
    /// </summary>
    /// <returns>Message what contains error information.</returns>
    public static string CommandNotExists() => "Expression contains invalid command.";
    
    /// <summary>
    /// Error message.
    /// </summary>
    /// <returns>Message what contains error information.</returns>
    public static string IncorrectInput() => "Incorrect input.";

    /// <summary>
    /// Error message.
    /// </summary>
    /// <returns>Message what contains error information.</returns>
    public static string IncompleteOperation() =>
        "The operation could not be performed. Make sure the entered data is correct.";
    
    /// <summary>
    /// Error message.
    /// </summary>
    /// <returns>Message what contains error information.</returns>
    public static string IdNotFound() => "Employee not found.";
    
    /// <summary>
    /// Information message.
    /// </summary>
    /// <returns>Message what contains recommend information.</returns>
    public static string Recommendation() => "Use 'help' command for view command list.";
}
