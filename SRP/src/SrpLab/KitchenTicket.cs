
namespace SrpLab;

public sealed class KitchenTicket
{
    private readonly List<(string Item, List<string> Ingredients, int PrepMinutes)> _items = new();

public IReadOnlyList<(string Item, List<string> Ingredients, int PrepMinutes)> Items => _items;
    public void AddItem(
        string item,
        IEnumerable<string> ingredients,
        int prepMinutes)
    {
        _items.Add((
            item,
            ingredients.Select(i => i.Trim().ToLowerInvariant()).ToList(),
            prepMinutes));
    }
}
