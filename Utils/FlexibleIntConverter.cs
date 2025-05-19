using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CovenWWDC23Port.Utils;

public class FlexibleIntConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            if (reader.TryGetInt32(out int i))
                return i;
            if (reader.TryGetDouble(out double d))
                return (int)d; // or throw if you want exactness
        }
        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}