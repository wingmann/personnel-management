namespace PersonnelManagementSystem.Employment;

/// <summary>
/// Salary calculation manager.
/// </summary>
public static class SalaryManager
{
    /// <summary>
    /// Calculate monthly salary.
    /// </summary>
    /// <param name="date">Date of employment.</param>
    /// <param name="baseSalary">Base employee salary count.</param>
    /// <param name="percent">Additional percent per year.</param>
    /// <param name="upperBound">Max count with additional percent.</param>
    /// <param name="bonusParts">Optional. Bonus parts from subordinates.</param>
    /// <returns>Monthly salary.</returns>
    public static decimal Calculate(DateTime date, decimal baseSalary, int percent, int upperBound, decimal bonusParts = 0M)
    {
        var result = baseSalary + bonusParts;
        var years = DateTime.Now.Year - date.Year;

        if (years > 0)
        {
            var max = WithPercentUp(baseSalary, upperBound);
            var percentPerYear = WithPercentUp(baseSalary, percent);
            var amount = 0M;

            for (int i = years; i > 0; i--)
            {
                var temp = amount + percentPerYear;

                if (temp > max)
                {
                    break;
                }
                
                amount += temp;
            }

            return result + amount;
        }

        return result;
    }

    private static decimal WithPercentUp(decimal baseCount, int percent) => baseCount / 100 * percent;
}
