using ReceiptProcessor.Models;
using ReceiptProcessor.Services.Abstractions;

namespace ReceiptProcessor.UnitTests;

public class ReceiptProcessorTests
{
    private readonly IReceiptProcessor _sut = new Services.ReceiptProcessor();

    [Theory]
    [MemberData(nameof(GetRecipesData))]
    public void Evaluation_ReturnsExpected(Receipt receipt, int expected)
    {
        //arrange

        //act
        var actual = _sut.Process(receipt);

        //assert
        Assert.Equal(expected, actual);
    }

    public static IEnumerable<object[]> GetRecipesData()
    {
        yield return new object[]
        {
            new Receipt
            {
                Retailer = "Target",
                PurchaseDate = new DateOnly(2022, 1, 1),
                PurchaseTime = new TimeOnly(13, 1, 0),
                Total = 35.35m,
                Items =
                [
                    new Item { Price = 6.49m, ShortDescription = "Mountain Dew 12PK" },
                    new Item { Price = 12.25m, ShortDescription = "Emils Cheese Pizza" },
                    new Item { Price = 1.26m, ShortDescription = "Knorr Creamy Chicken" },
                    new Item { Price = 3.35m, ShortDescription = "Doritos Nacho Cheese" },
                    new Item { Price = 12.00m, ShortDescription = "   Klarbrunn 12-PK 12 FL OZ  " }
                ]
            },
            28
        };

        yield return new object[]
        {
            new Receipt
            {
                Retailer = "M&M Corner Market",
                PurchaseDate = new DateOnly(2022, 3, 20),
                PurchaseTime = new TimeOnly(14, 33, 0),
                Total = 9.00m,
                Items =
                [
                    new Item { Price = 2.25m, ShortDescription = "Gatorade" },
                    new Item { Price = 2.25m, ShortDescription = "Gatorade" },
                    new Item { Price = 2.25m, ShortDescription = "Gatorade" },
                    new Item { Price = 2.25m, ShortDescription = "Gatorade" }
                ]
            },
            109
        };
    }
}