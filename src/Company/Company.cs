using System.Text;
using CommandHandler;
using CommandHandler.Interfaces;
using Company.Interfaces;
using Employment;
using Employment.Interfaces;

namespace Company;

public class Company : ICompany
{
    private readonly string _name;

    private readonly string _description;

    private readonly IStaffManager _staffManager;

    private readonly ICommandProcessor _commandProcessor;

    public Company()
    {
        _name = Input("Organization name");
        _description = Input("Description", false);

        _staffManager = new StaffManager();
        _commandProcessor = new CommandProcessor();

        MessageWriter.Write(Essentials.Recommendation(), 'i', true);
    }
    
    public void Startup()
    {
        while (true)
        {
            Operation operation;

            try
            {
                operation = _commandProcessor.Startup();
            }
            catch (ArgumentException ex)
            {
                MessageWriter.Write(ex.Message, '!');
                continue;
            }

            if (!Execute(operation))
            {
                MessageWriter.Write(Essentials.IncompleteOperation(), '!');
            }
        }
    }
    
    private bool Execute(Operation operation)
    {
        switch (operation)
        {
            case Operation.Help:
                Console.WriteLine(Essentials.CommandList());
                break;
            case Operation.ShowCompanyInfo:
                Console.WriteLine(ToString());
                break;
            case Operation.ShowEmployeeList:
                Console.WriteLine(GetEmployeeList());
                break;
            case Operation.ShowEmployeeCount:
                Console.WriteLine($"Employee count: {EmployeeCount()}");
                break;
            case Operation.ShowSalary:
                try
                {
                    var result = GetSalary(Input("ID"));
                    Console.WriteLine($"USD {result}");
                }
                catch (ArgumentException)
                {
                    MessageWriter.Write(Essentials.IdNotFound(), '!');
                }

                break;
            case Operation.ShowAllSalary:
                Console.WriteLine($"Total employees salary: {GetAllSalary()}");
                break;
            case Operation.AddEmployee:
                AddEmployee(new Person(Input("First name"), Input("Last name")), EmployeeType.Employee);
                break;
            case Operation.AddSales:
                AddEmployee(new Person(Input("First name"), Input("Last name")), EmployeeType.Sales);
                break;
            case Operation.AddManager:
                AddEmployee(new Person(Input("First name"), Input("Last name")), EmployeeType.Manager);
                break;
            case Operation.AddSubordinateEmployee:
                return TryAddSubordinate(
                    new Person(Input("First name"), Input("Last name")),
                    Input("Superior ID"),
                    EmployeeType.Employee);
            case Operation.AddSubordinateSales:
                return TryAddSubordinate(
                    new Person(Input("First name"), Input("Last name")),
                    Input("Superior ID"),
                    EmployeeType.Sales);
            case Operation.RemoveEmployee:
            {
                if (!TryRemove(Input("ID")))
                {
                    MessageWriter.Write(Essentials.IdNotFound(), '!');
                }

                break;
            }
            case Operation.Clear:
                Console.Clear();
                break;
            default:
                return false;
        }

        return true;
    }

    private static string Input(string message, bool notNullRequired = true)
    {
        while (true)
        {
            Console.Write($"{message}: ");
            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim(' ');
            }

            if (!notNullRequired)
            {
                return "Empty";
            }
            
            MessageWriter.Write(Essentials.IncorrectInput(), '!');
        }
    }

    private static string GenerateId() => Guid.NewGuid().ToString();

    private void AddEmployee(Person person, EmployeeType type)
    {
        switch (type)
        {
            case EmployeeType.Employee:
                _staffManager.Add(person, GenerateId(), DateTime.Today, type);
                break;
            case EmployeeType.Sales:
                _staffManager.Add(person, GenerateId(), DateTime.Today, type);
                break;
            case EmployeeType.Manager:
                _staffManager.Add(person, GenerateId(), DateTime.Today, type);
                break;
        }
    }

    private bool TryAddSubordinate(Person person, string superiorId, EmployeeType type)
    {
        var id = GenerateId();
        return type switch
        {
            EmployeeType.Employee => _staffManager.TryAddSubordinate(person, id, DateTime.Today, superiorId, type),
            EmployeeType.Sales => _staffManager.TryAddSubordinate(person, id, DateTime.Today, superiorId, type),
            _ => false
        };
    }

    private bool TryRemove(string id) => _staffManager.TryRemove(id);

    private int EmployeeCount() => _staffManager.EmployeeCount();

    private decimal GetSalary(string id) => _staffManager.GetSalary(id) ?? throw new ArgumentException(null);

    private decimal GetAllSalary() => _staffManager.GetAllSalary();

    private string GetEmployeeList() => _staffManager.GetEmployeeList();

    private string GetCompanyInformation()
    {
        StringBuilder sb = new();
        sb.AppendLine("\nCompany information ----------------------------------------------------------");
        sb.AppendLine($"| Name:        {_name}");
        sb.AppendLine($"| Description: {_description}");
        sb.AppendLine($" -----------------------------------------------------------------------------");

        return sb.ToString();
    }

    public override string ToString() => GetCompanyInformation();
}