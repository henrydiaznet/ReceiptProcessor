using ReceiptProcessor.Models;
using ReceiptProcessor.Services.Abstractions;

namespace ReceiptProcessor.Services;

public class ReceiptProcessor : IReceiptProcessor
{
    public int Process(Receipt receipt)
    {
        return EvaluateAllRules(receipt).Sum();
    }

    private IEnumerable<int> EvaluateAllRules(Receipt receipt)
    {
        return Evaluations.All.Select(func => func(receipt));
    }
}