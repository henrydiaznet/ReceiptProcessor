using ReceiptProcessor.Models;

namespace ReceiptProcessor.Services.Abstractions;

public interface IStorage
{
    Guid Add(Receipt receipt);
    Receipt? Find(Guid id);
    IEnumerable<Receipt> GetAll();
}