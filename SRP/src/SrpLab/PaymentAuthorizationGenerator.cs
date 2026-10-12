
namespace SrpLab;

public sealed class PaymentAuthorizationGenerator
{
    public string Generate(
        decimal grandTotal,
        string cardLast4,
        int lineCount)
    {
        var payload =
            $"{grandTotal:0.00}|{cardLast4}|{lineCount}";

        var hash = payload.GetHashCode();

        return $"AUTH-{Math.Abs(hash):X8}";
    }
}
