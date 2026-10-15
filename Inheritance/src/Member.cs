
namespace LibrarySystem;

public class Member : Person
{
    private readonly List<Loan> _loans = new();

    public IReadOnlyList<Loan> Loans => _loans.AsReadOnly();

    public int MaxLoans { get; }
    public decimal DiscountPercentage { get; }

    protected Member(
        string personId,
        string fullName,
        string phone,
        int maxLoans,
        decimal discountPercentage)
        : base(personId, fullName, phone)
    {
        if (maxLoans <= 0)
            throw new ArgumentException("Maximum loans must be positive.");

        if (discountPercentage < 0 || discountPercentage > 100)
            throw new ArgumentException(
                "Discount percentage must be between 0 and 100.");

        MaxLoans = maxLoans;
        DiscountPercentage = discountPercentage;
    }

    public Loan BorrowItem(
        LibraryItem item,
        DateTime borrowDate,
        string loanId)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        if (string.IsNullOrWhiteSpace(loanId))
            throw new ArgumentException("Loan ID cannot be empty.");

        int activeLoans = 0;

        foreach (Loan loan in _loans)
        {
            if (loan.Status == LoanStatus.Borrowed)
                activeLoans++;
        }

        if (activeLoans >= MaxLoans)
            throw new InvalidOperationException(
                "Member has reached the maximum loan limit.");

        if (item.IsWithdrawn)
            throw new InvalidOperationException(
                "A withdrawn item cannot be borrowed.");

        if (item.IsOnLoan)
            throw new InvalidOperationException(
                "This item is already on loan.");

        Loan newLoan = new Loan(loanId, borrowDate, this, item);

        _loans.Add(newLoan);
        item.IsOnLoan = true;

        return newLoan;
    }
}
