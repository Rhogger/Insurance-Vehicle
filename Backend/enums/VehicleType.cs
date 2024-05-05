using System.Text.Json.Serialization;

namespace Backend.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum VehicleType
{
  sedan,
  sports_car
}
