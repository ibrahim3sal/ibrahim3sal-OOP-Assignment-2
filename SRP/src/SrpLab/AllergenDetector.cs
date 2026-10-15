
namespace SrpLab;

public sealed class AllergenDetector
{
    public IReadOnlyList<string> Detect(
        IEnumerable<(string Item, List<string> Ingredients, int PrepMinutes)> items)
    {
        var hits = new HashSet<string>(
            StringComparer.OrdinalIgnoreCase);

        foreach (var (_, ingredients, _) in items)
        {
            foreach (var ingredient in ingredients)
            {
                if (ingredient.Contains("milk") ||
                    ingredient.Contains("cheese") ||
                    ingredient.Contains("butter"))
                {
                    hits.Add("dairy");
                }

                if (ingredient.Contains("wheat") ||
                    ingredient.Contains("flour") ||
                    ingredient.Contains("bread"))
                {
                    hits.Add("gluten");
                }

                if (ingredient.Contains("peanut") ||
                    ingredient.Contains("almond") ||
                    ingredient.Contains("cashew"))
                {
                    hits.Add("nuts");
                }

                if (ingredient.Contains("shrimp") ||
                    ingredient.Contains("prawn") ||
                    ingredient.Contains("crab"))
                {
                    hits.Add("shellfish");
                }
            }
        }

        return hits.OrderBy(x => x).ToList();
    }
}
