namespace ReceiptProcessor.Models;

public class Receipt
{
    public Guid? Id { get; set; }
    public string Retailer { get; init; }
    public DateOnly PurchaseDate { get; init; }
    public TimeOnly PurchaseTime { get; init; }
    public List<Item> Items { get; init; } = [];
    public decimal Total { get; init; }
}

public class Item
{
    public string ShortDescription { get; init; }
    public decimal Price { get; init; }
}