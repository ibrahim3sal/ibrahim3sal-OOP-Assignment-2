
namespace LibrarySystem;

public class Book : LibraryItem
{
    public string Author { get; }

    public Book(
        string catalogNumber,
        string title,
        string author,
        decimal baseLateFee)
        : base(
            catalogNumber,
            title,
            baseLateFee,
            loanPeriodDays: 21,
            feeMultiplier: 1m)
    {
        if (string.IsNullOrWhiteSpace(author))
            throw new ArgumentException("Author cannot be empty.");

        Author = author;
    }
}
