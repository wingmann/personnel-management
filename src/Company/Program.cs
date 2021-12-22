using PersonnelManagementSystem.Company.Interfaces;

namespace PersonnelManagementSystem.Company;

internal static class Program
{
    private static void Main(string[] args)
    {
        ICompany company = new Company();
        company.Startup();
    }
}
