
namespace SrpLab;

public sealed class GiftWrapPricing
{
    public decimal CalculateFee(bool giftWrapEnabled)
    {
        return giftWrapEnabled ? 4.99m : 0m;
    }
}
