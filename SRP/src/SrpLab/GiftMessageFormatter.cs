
namespace SrpLab;

public sealed class GiftMessageFormatter
{
    public string Format(
        IEnumerable<string> itemSkus,
        string fromName,
        decimal grandTotal)
    {
        var items = string.Join(", ", itemSkus);

        return $"Dear friend,\nA gift from {fromName} awaits ({items}).\nTotal surprise value: {grandTotal:C}\n";
    }
}
