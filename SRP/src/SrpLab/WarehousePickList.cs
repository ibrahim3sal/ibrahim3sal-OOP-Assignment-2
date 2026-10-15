
namespace SrpLab;

public sealed class WarehousePickList
{
    private readonly List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> _lines = new();

    private readonly StockAllocationCalculator _allocationCalculator = new();
    private readonly WarehouseWalkingOrderCalculator _walkingOrderCalculator = new();
    private readonly PickerInstructionFormatter _instructionFormatter = new();
    private readonly WmsXmlExporter _xmlExporter = new();

    public void AddNeed(
        string sku,
        string aisle,
        int bin,
        int qtyNeeded,
        int qtyOnHand)
    {
        _lines.Add((sku, aisle, bin, qtyNeeded, qtyOnHand));
    }

    public IReadOnlyList<(string Sku, int Allocated)> Allocate()
    {
        return _allocationCalculator.Allocate(_lines);
    }

    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> WalkingOrder()
    {
        return _walkingOrderCalculator.Calculate(_lines);
    }

    public string PickerScript()
    {
        return _instructionFormatter.Format(
            WalkingOrder(),
            Allocate(),
            _lines);
    }

    public string WmsXmlBatch(string batchId)
    {
        return _xmlExporter.Export(batchId, Allocate());
    }
}
