namespace CommandHandler.Interfaces;

/// <summary>
/// Console command processor.
/// </summary>
public interface ICommandProcessor
{
    /// <summary>
    /// Runs the algorithm for processing a commands from the console.
    /// </summary>
    /// <returns>Operation type to executor.</returns>
    Operation Startup();
}