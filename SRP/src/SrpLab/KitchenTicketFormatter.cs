
namespace SrpLab;

public sealed class KitchenTicketFormatter
{
    public string RenderThermalTicket(
        int orderNumber,
        IReadOnlyList<(string Item, List<string> Ingredients, int PrepMinutes)> items,
        IReadOnlyList<string> allergens,
        int estimatedReadyMinutes)
    {
        var width = 32;
        var line = new string('=', width);

        var body = string.Join(
            '\n',
            items.Select(item =>
                $"* {item.Item.ToUpperInvariant()} ({item.PrepMinutes}m)"));

        var allergyLine = allergens.Count == 0
            ? "ALLERGENS: none"
            : "ALLERGENS: " + string.Join(",", allergens);

        return $"{line}\nORDER #{orderNumber}\nETA {estimatedReadyMinutes} MIN\n{body}\n{allergyLine}\n{line}\n";
    }
}
