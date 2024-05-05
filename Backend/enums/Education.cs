using System.Text.Json.Serialization;

namespace Backend.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Education
{
  high_school,
  none,
  university
}
