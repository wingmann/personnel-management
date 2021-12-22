using PersonnelManagementSystem.CommandHandler.Interfaces;

namespace PersonnelManagementSystem.CommandHandler;

/// <summary>
/// Console command processor.
/// </summary>
public sealed class CommandProcessor : ICommandProcessor
{
    /// <summary>
    /// Runs the algorithm for processing commands from the console.
    /// </summary>
    /// <returns>Operation type to executor.</returns>
    public Operation Startup() => Parse(InputCommand());
    
    private static string InputCommand()
    {
        while (true)
        {
            Console.Write("[command]: ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }

            return input.Trim(' ');
        }
    }
    
    private static Operation Parse(string command)
    {
        var tokens = command.ToUpper().Trim(' ').Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var length = tokens.Length;

        switch (length)
        {
            case 1 when tokens[0] == "HELP":
                return Operation.Help;
            case 1 when tokens[0] == "RM" || tokens[0] == "REMOVE":
                return Operation.RemoveEmployee;
            case 1 when tokens[0] == "CLEAR":
                return Operation.Clear;
            case 1 when tokens[0] == "SALARY":
                return Operation.ShowSalary;
            case 1 when tokens[0] == "COUNT":
                return Operation.ShowEmployeeCount;
            case > 1 when tokens[0] == "SALARY" && length == 2 && tokens[1] == "ALL":
                return Operation.ShowAllSalary;
            case > 1 when tokens[0] == "SHOW" && length == 2:
            {
                switch (tokens[1])
                {
                    case "INFO":
                        return Operation.ShowCompanyInfo;
                    case "ALL":
                        return Operation.ShowEmployeeList;
                }
                
                break;
            }
            case > 1 when tokens[0] == "ADD":
            {
                switch (length)
                {
                    case 2 when tokens[1] == "EMPLOYEE":
                        return Operation.AddEmployee;
                    case 2 when tokens[1] == "SALES":
                        return Operation.AddSales;
                    case 2 when tokens[1] == "MANAGER":
                        return Operation.AddManager;
                    case 3 when tokens[1] == "SUB":
                    {
                        switch (tokens[2])
                        {
                            case "EMPLOYEE":
                                return Operation.AddSubordinateEmployee;
                            case "SALES":
                                return Operation.AddSubordinateSales;
                        }

                        break;
                    }
                }

                break;
            }
        }
        
        throw new ArgumentException(Essentials.CommandNotExists());
    }
}
