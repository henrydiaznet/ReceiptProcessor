using ReceiptProcessor.Models;

namespace ReceiptProcessor.Services;

public static class Evaluations
{
    public static readonly List<Func<Receipt, int>> All;

    private static readonly Func<Receipt, int> RetailerName = receipt =>
        receipt.Retailer.ToCharArray().Count(char.IsLetterOrDigit);

    private static readonly Func<Receipt, int> TotalRound = receipt =>
        receipt.Total == Math.Round(receipt.Total) ? 50 : 0;

    private static readonly Func<Receipt, int> TotalQuarter = receipt => receipt.Total % 0.25m == 0 ? 25 : 0;

    private static readonly Func<Receipt, int> ItemsCount = receipt => receipt.Items.Count / 2 * 5;

    private static readonly Func<Receipt, int> ItemDescription = receipt =>
    {
        var points = 0m;
        foreach (var item in receipt.Items)
            if (item.ShortDescription.Trim().Length % 3 == 0)
                points += Math.Ceiling(item.Price * 0.2m);

        return (int)points;
    };

    private static readonly Func<Receipt, int> DayIsOdd = receipt => receipt.PurchaseDate.Day % 2 == 1 ? 6 : 0;

    private static readonly Func<Receipt, int> TimeOfPurchase =
        receipt => receipt.PurchaseTime.Hour is >= 14 and < 16 ? 10 : 0;

    static Evaluations()
    {
        All =
        [
            RetailerName,
            TotalRound,
            TotalQuarter,
            ItemsCount,
            ItemDescription,
            DayIsOdd,
            TimeOfPurchase
        ];
    }
}