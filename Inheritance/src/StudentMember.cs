
namespace LibrarySystem;

public class StudentMember : Member
{
    public StudentMember(
        string personId,
        string fullName,
        string phone)
        : base(
            personId,
            fullName,
            phone,
            maxLoans: 3,
            discountPercentage: 0)
    {
    }
}
