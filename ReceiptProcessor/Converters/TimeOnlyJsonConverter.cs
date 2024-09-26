using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReceiptProcessor.Converters;

public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    private const string TimeFormat = "HH:mm";

    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var timeString = reader.GetString();
        if (TimeOnly.TryParseExact(timeString, TimeFormat, out var result)) return result;

        throw new JsonException($"Invalid time format: {timeString}. Expected format: {TimeFormat}");
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(TimeFormat));
    }
}