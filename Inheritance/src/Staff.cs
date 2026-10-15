
namespace LibrarySystem;

public class Staff : Person
{
    public DateTime HireDate { get; }

    public decimal MonthlySalary { get; private set; }

    private decimal Allowance { get; }

    protected Staff(
        string personId,
        string fullName,
        string phone,
        DateTime hireDate,
        decimal monthlySalary,
        decimal allowance)
        : base(personId, fullName, phone)
    {
        if (hireDate == default)
            throw new ArgumentException("Hire date is invalid.");

        if (monthlySalary <= 0)
            throw new ArgumentException("Monthly salary must be positive.");

        if (allowance < 0)
            throw new ArgumentException("Allowance cannot be negative.");

        HireDate = hireDate;
        MonthlySalary = monthlySalary;
        Allowance = allowance;
    }

    public decimal MonthlyPay()
    {
        return MonthlySalary + Allowance;
    }

    public void GiveRaise(decimal percentage)
    {
        if (percentage <= 0)
            throw new ArgumentException(
                "Raise percentage must be greater than zero.");

        MonthlySalary += MonthlySalary * percentage / 100;
    }
}
