using System.Text.Json.Serialization;

namespace Backend.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Income
{
  middle_class,
  poverty,
  upper_class,
  working_class
}
