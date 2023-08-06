using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shezzy.Shared.Infrastructure.Json
{
  public class NullableDateTimeConverter : JsonConverter<DateTime?>
  {
    private const string DateFormat = "yyyy-MM-dd";

    public override DateTime? Read(
      ref Utf8JsonReader reader,
      Type typeToConvert,
      JsonSerializerOptions options)
    {
      var value = reader.GetString();

      return string.IsNullOrEmpty(value) ? null : DateTime.TryParse(value, out var result) ? result : default;
    }

    public override void Write(
      Utf8JsonWriter writer,
      DateTime? value,
      JsonSerializerOptions options) => writer.WriteStringValue(value == null ? string.Empty : value.Value.ToString(DateFormat));
  }
}