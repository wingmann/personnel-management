using Company.Interfaces;

namespace Company;

internal static class Program
{
    private static void Main(string[] args)
    {
        ICompany company = new global::Company.Company();
        company.Startup();
    }
}
