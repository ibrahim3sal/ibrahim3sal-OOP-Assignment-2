
namespace SrpLab;

public sealed class WmsXmlExporter
{
    public string Export(
        string batchId,
        IReadOnlyList<(string Sku, int Allocated)> allocations)
    {
        var parts = allocations.Select(
            item => $"<line sku=\"{item.Sku}\" qty=\"{item.Allocated}\" />");

        return $"<batch id=\"{batchId}\">{string.Join("", parts)}</batch>";
    }
}
