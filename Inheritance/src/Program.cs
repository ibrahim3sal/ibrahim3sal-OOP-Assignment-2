
using LibrarySystem;

Console.WriteLine("=== Library System ===");

// Create library items
Book book = new Book("B001", "Clean Code", "Robert Martin", 10m);
DVD dvd = new DVD("D001", "C# Fundamentals", 20m);
Magazine magazine = new Magazine("M001", "Tech Monthly", 8m);

// Create members
StudentMember student = new StudentMember(
    "P001", "Ahmed Ali", "01000000001");

PremiumMember premium = new PremiumMember(
    "P002", "Mohamed Hassan", "01000000002", 10m);

// Create staff
Librarian librarian = new Librarian(
    "S001", "Sara Ali", "01000000003",
    new DateTime(2023, 1, 1), 8000m);

HeadLibrarian headLibrarian = new HeadLibrarian(
    "S002", "Omar Hassan", "01000000004",
    new DateTime(2020, 1, 1), 12000m);

Shelver shelver = new Shelver(
    "S003", "Mona Ahmed", "01000000005",
    new DateTime(2024, 1, 1), 6000m, "A");

// Borrow a DVD
DateTime borrowDate = new DateTime(2026, 1, 1);

Loan loan = premium.BorrowItem(
    dvd, borrowDate, "L001");

Console.WriteLine($"Borrow date: {loan.BorrowDate:d}");
Console.WriteLine($"Due date: {loan.DueDate:d}");

// Return it five days late
DateTime returnDate = loan.DueDate.AddDays(5);

librarian.ProcessReturn(loan, returnDate);

Console.WriteLine($"Return date: {loan.ReturnDate:d}");
Console.WriteLine($"Late fee: {loan.LateFee:C}");
Console.WriteLine($"Reading points: {premium.ReadingPoints}");

// Test staff monthly pay
List<Staff> staffMembers = new()
{
    librarian,
    headLibrarian,
    shelver
};

Console.WriteLine("\n=== Monthly Pay ===");

foreach (Staff staff in staffMembers)
{
    Console.WriteLine(
        $"{staff.FullName}: {staff.MonthlyPay():C}");
}

// Test item loan periods and daily fees
List<LibraryItem> items = new()
{
    book,
    dvd,
    magazine
};

Console.WriteLine("\n=== Library Items ===");

foreach (LibraryItem item in items)
{
    Console.WriteLine(
        $"{item.Title}: {item.LoanPeriodDays} days, " +
        $"daily late fee = {item.DailyLateFee():C}");
}

// Test invalid operations
Console.WriteLine("\n=== Exception Tests ===");

try
{
    librarian.ProcessReturn(loan, returnDate);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Duplicate return rejected: {ex.Message}");
}

try
{
    librarian.MarkItemAsLost(loan);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Invalid lost status rejected: {ex.Message}");
}

// Test changing an item's late fee
headLibrarian.ChangeLateFee(book, 15m);

Console.WriteLine(
    $"\nUpdated book daily fee: {book.DailyLateFee():C}");

// Test Shelver reassignment
shelver.Reassign("B");

Console.WriteLine($"New shelver section: {shelver.Section}");

// Test salary raise
librarian.GiveRaise(10m);

Console.WriteLine(
    $"Librarian salary after raise: {librarian.MonthlySalary:C}");


// 1. Student cannot exceed the loan limit of 3
try
{
    StudentMember student2 = new StudentMember(
        "P010", "Test Student", "01000000010");

    Book book1 = new Book("B010", "Book One", "Author One", 10m);
    Book book2 = new Book("B011", "Book Two", "Author Two", 10m);
    Book book3 = new Book("B012", "Book Three", "Author Three", 10m);
    Book book4 = new Book("B013", "Book Four", "Author Four", 10m);

    student2.BorrowItem(book1, borrowDate, "L010");
    student2.BorrowItem(book2, borrowDate, "L011");
    student2.BorrowItem(book3, borrowDate, "L012");

    student2.BorrowItem(book4, borrowDate, "L013");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Loan limit enforced: {ex.Message}");
}

// 2. An item already on loan cannot be borrowed again
try
{
    StudentMember student3 = new StudentMember(
        "P020", "Another Student", "01000000020");

    student3.BorrowItem(dvd, borrowDate, "L020");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Duplicate borrowing rejected: {ex.Message}");
}

// 3. A withdrawn item cannot be borrowed
try
{
    Book withdrawnBook = new Book(
        "B020", "Withdrawn Book", "Author X", 10m);

    headLibrarian.WithdrawItem(withdrawnBook);

    StudentMember student4 = new StudentMember(
        "P030", "Third Student", "01000000030");

    student4.BorrowItem(withdrawnBook, borrowDate, "L030");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Withdrawn item rejected: {ex.Message}");
}

// 4. A zero or negative raise must be rejected
try
{
    librarian.GiveRaise(0);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Invalid raise rejected: {ex.Message}");
}

// 5. Test that borrowing an item already on loan is rejected.
try
{
    Book sharedBook = new Book(
        "B030", "Shared Book", "Author Y", 10m);

    StudentMember firstStudent = new StudentMember(
        "P040", "First Student", "01000000040");

    StudentMember secondStudent = new StudentMember(
        "P041", "Second Student", "01000000041");

    firstStudent.BorrowItem(sharedBook, borrowDate, "L040");

    secondStudent.BorrowItem(sharedBook, borrowDate, "L041");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(
        $"Duplicate borrowing rejected: {ex.Message}");
}
