using Microsoft.AspNetCore.Http.Json;
using ReceiptProcessor.Converters;
using ReceiptProcessor.Models;
using ReceiptProcessor.Services;
using ReceiptProcessor.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IStorage, InMemoryStorage>();
builder.Services.AddScoped<IReceiptProcessor, ReceiptProcessor.Services.ReceiptProcessor>();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/receipts/", (IStorage storage) => Results.Ok(storage.GetAll()))
    .WithName("GetAllReceipts")
    .WithOpenApi();

app.MapPost("/receipts/process", (Receipt receipt, IStorage storage) =>
    {
        var id = storage.Add(receipt);
        return Results.Ok(new { id });
    })

    .WithOpenApi();

app.MapGet("/receipts/{id:guid}/points", (Guid id, IStorage storage, IReceiptProcessor receiptProcessor) =>
    {
        var receipt = storage.Find(id);
        return receipt is null ? Results.NotFound() : Results.Ok(new { points = receiptProcessor.Process(receipt) });
    })
    .WithName("GetPointsForReceipt")
    .WithOpenApi();


app.Run();