using ReceiptProcessor.Models;
using ReceiptProcessor.Services.Abstractions;

namespace ReceiptProcessor.Services;

public class InMemoryStorage : IStorage
{
    private readonly IDictionary<Guid, Receipt> _dictionary = new Dictionary<Guid, Receipt>();

    public Guid Add(Receipt receipt)
    {
        var id = Guid.NewGuid();
        receipt.Id = id;
        _dictionary.Add(id, receipt);
        return id;
    }

    public Receipt? Find(Guid id)
    {
        return _dictionary.ContainsKey(id) ? _dictionary[id] : null;
    }

    public IEnumerable<Receipt> GetAll()
    {
        return _dictionary.Values;
    }
}