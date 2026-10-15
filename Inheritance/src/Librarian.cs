
namespace LibrarySystem;

public class Librarian : Staff
{
    public Librarian(
        string personId,
        string fullName,
        string phone,
        DateTime hireDate,
        decimal monthlySalary)
        : base(
            personId,
            fullName,
            phone,
            hireDate,
            monthlySalary,
            allowance: 0)
    {
    }

    public void ProcessReturn(Loan loan, DateTime returnDate)
    {
        if (loan == null)
            throw new ArgumentNullException(nameof(loan));

        loan.Return(returnDate);
    }

    public void MarkItemAsLost(Loan loan)
    {
        if (loan == null)
            throw new ArgumentNullException(nameof(loan));

        loan.MarkAsLost();
    }
}
