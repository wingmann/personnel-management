namespace PersonnelManagementSystem.CommandHandler;

/// <summary>
/// Information and error message writer.
/// </summary>
public static class MessageWriter
{
    /// <summary>
    /// Write message with new line after.
    /// </summary>s
    /// <param name="message">Message.</param>
    /// <param name="symbol">Character that indicates the type of message.</param>
    /// <param name="lineBefore">Allow newline before message.</param>
    public static void Write(string message, char symbol, bool lineBefore = false)
    {
        // In case newline is allowed before the message.
        if (lineBefore)
        {
            Console.WriteLine();
        }

        Console.WriteLine($"[{symbol}] {message}");
    }
}