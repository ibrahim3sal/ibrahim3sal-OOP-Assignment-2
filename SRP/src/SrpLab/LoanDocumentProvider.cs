
namespace SrpLab;

public sealed class LoanDocumentProvider
{
    public IReadOnlyList<string> RequiredDocuments(
        decimal requestedAmount,
        int employmentMonths,
        bool hasCollateral,
        bool isEligible)
    {
        var documents = new List<string>
        {
            "National ID",
            "Proof of income (3 months)"
        };

        if (requestedAmount > 40_000m)
            documents.Add("Bank statements (6 months)");

        if (hasCollateral)
            documents.Add("Collateral ownership deed");

        if (employmentMonths < 12)
            documents.Add("Employer letter");

        if (!isEligible)
            documents.Add("Manual underwriter referral form");

        return documents;
    }
}
