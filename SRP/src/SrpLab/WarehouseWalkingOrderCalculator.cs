
namespace SrpLab;

public sealed class WarehouseWalkingOrderCalculator
{
    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> Calculate(
        IReadOnlyList<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> lines)
    {
        return lines
            .OrderBy(line => line.Aisle)
            .ThenBy(line => line.Bin)
            .Select(line => (
                line.Aisle,
                line.Bin,
                line.Sku,
                Math.Min(line.QtyNeeded, line.QtyOnHand)))
            .Where(line => line.Item4 > 0)
            .ToList();
    }
}
