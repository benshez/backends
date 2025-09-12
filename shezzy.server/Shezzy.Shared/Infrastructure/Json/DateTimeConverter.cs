using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shezzy.Shared.Infrastructure.Json
{
  public class DateTimeConverter : JsonConverter<DateTime>
  {
    private const string DateFormat = "yyyy-MM-dd";
    public override DateTime Read(
      ref Utf8JsonReader reader,
      Type typeToConvert,
      JsonSerializerOptions options)
    {
      var value = reader.GetString();

      return DateTime.TryParse(value, out var result) ? result : default;
    }

    public override void Write(
      Utf8JsonWriter writer,
      DateTime value,
      JsonSerializerOptions options) => writer.WriteStringValue(value.ToString(DateFormat));
  }
}