
namespace SrpLab;

public sealed class KitchenPreparationEstimator
{
    public int EstimateReadyMinutes(
        IReadOnlyList<(string Item, List<string> Ingredients, int PrepMinutes)> items,
        int openStations,
        int allergenCount)
    {
        if (openStations <= 0)
            openStations = 1;

        var sequential = items.Sum(item => item.PrepMinutes);

        var parallel = (int)Math.Ceiling(
            sequential / (double)openStations);

        if (allergenCount > 0)
            parallel += 3;

        var longest = items.Count == 0
            ? 0
            : items.Max(item => item.PrepMinutes);

        return Math.Max(parallel, longest);
    }

    public string DetermineExpoLane(
        int allergenCount,
        int estimatedReadyMinutes)
    {
        if (allergenCount > 0)
            return "LANE-ALLERGY";

        return estimatedReadyMinutes > 20
            ? "LANE-SLOW"
            : "LANE-FAST";
    }
}
