
namespace LibrarySystem;

public class LibraryItem
{
    public string CatalogNumber { get; }
    public string Title { get; }

    public decimal BaseLateFee { get; private set; }
    public int LoanPeriodDays { get; }

    public bool IsWithdrawn { get; private set; }

    public bool IsOnLoan { get; internal set; }

    private decimal FeeMultiplier { get; }

    protected LibraryItem(
        string catalogNumber,
        string title,
        decimal baseLateFee,
        int loanPeriodDays,
        decimal feeMultiplier)
    {
        if (string.IsNullOrWhiteSpace(catalogNumber))
            throw new ArgumentException("Catalog number cannot be empty.");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.");

        if (baseLateFee <= 0)
            throw new ArgumentException("Base late fee must be positive.");

        if (loanPeriodDays <= 0)
            throw new ArgumentException("Loan period must be positive.");

        if (feeMultiplier <= 0)
            throw new ArgumentException("Fee multiplier must be positive.");

        CatalogNumber = catalogNumber;
        Title = title;
        BaseLateFee = baseLateFee;
        LoanPeriodDays = loanPeriodDays;
        FeeMultiplier = feeMultiplier;
        IsWithdrawn = false;
        IsOnLoan = false;
    }

    public decimal DailyLateFee()
    {
        return BaseLateFee * FeeMultiplier;
    }

    public void ChangeBaseLateFee(decimal newFee)
    {
        if (newFee <= 0)
            throw new ArgumentException("Late fee must be greater than zero.");

        BaseLateFee = newFee;
    }

    public void Withdraw()
    {
        if (IsOnLoan)
            throw new InvalidOperationException(
                "An item on loan cannot be withdrawn.");

        IsWithdrawn = true;
    }

    public void Restore()
    {
        IsWithdrawn = false;
    }
}
