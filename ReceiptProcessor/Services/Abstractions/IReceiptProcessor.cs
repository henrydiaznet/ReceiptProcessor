using ReceiptProcessor.Models;

namespace ReceiptProcessor.Services.Abstractions;

public interface IReceiptProcessor
{
    int Process(Receipt receipt);
}