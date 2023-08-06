using System.Text.Json;

namespace Shezzy.Shared.Infrastructure.Json
{
  public static class JsonOptions
  {
    public static readonly JsonSerializerOptions Default = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      PropertyNameCaseInsensitive = true,
      Converters = { new DateTimeConverter(), new NullableDateTimeConverter(), new DoubleConverter() },
      Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
  }
}