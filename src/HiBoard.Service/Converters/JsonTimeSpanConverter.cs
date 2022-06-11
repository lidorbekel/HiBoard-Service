using System.Globalization;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace HiBoard.Service.Converters;

public class JsonTimeSpanConverter : JsonConverter<TimeSpan>
{
    public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }

    public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.Value is not null)
        {
            var s = (string) reader.Value!;
            return TimeSpan.Parse(s, CultureInfo.InvariantCulture);
        }

        return new TimeSpan(0, 0, 0, 0);
    }
}