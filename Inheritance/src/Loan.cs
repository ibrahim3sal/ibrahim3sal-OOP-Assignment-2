
namespace LibrarySystem;

public class Loan
{
    public string LoanId { get; }

    public DateTime BorrowDate { get; }

    public DateTime DueDate =>
        BorrowDate.AddDays(Item.LoanPeriodDays);

    public DateTime? ReturnDate { get; private set; }

    public Member Member { get; }

    public LibraryItem Item { get; }

    public LoanStatus Status { get; private set; }

    public decimal LateFee
    {
        get
        {
            if (Status != LoanStatus.Returned || ReturnDate == null)
                return 0;

            int daysLate = (ReturnDate.Value.Date - DueDate.Date).Days;

            if (daysLate <= 0)
                return 0;

            decimal originalFee = daysLate * Item.DailyLateFee();

            return originalFee *
                   (1 - Member.DiscountPercentage / 100m);
        }
    }

    public Loan(
        string loanId,
        DateTime borrowDate,
        Member member,
        LibraryItem item)
    {
        if (string.IsNullOrWhiteSpace(loanId))
            throw new ArgumentException("Loan ID cannot be empty.");

        if (borrowDate == default)
            throw new ArgumentException("Borrow date is invalid.");

        if (member == null)
            throw new ArgumentNullException(nameof(member));

        if (item == null)
            throw new ArgumentNullException(nameof(item));

        LoanId = loanId;
        BorrowDate = borrowDate;
        Member = member;
        Item = item;
        Status = LoanStatus.Borrowed;
    }

    public void Return(DateTime returnDate)
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException(
                "Only borrowed loans can be returned.");

        if (returnDate.Date < BorrowDate.Date)
            throw new ArgumentException(
                "Return date cannot precede borrow date.");

        ReturnDate = returnDate;
        Status = LoanStatus.Returned;
        Item.IsOnLoan = false;
    }

    public void MarkAsLost()
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException(
                "Only borrowed loans can be marked as lost.");

        Status = LoanStatus.Lost;
        Item.IsOnLoan = false;
    }
}
