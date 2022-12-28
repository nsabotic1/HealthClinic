using System.Text.Json.Serialization;

namespace HealthClinicApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Neuter = 3
    }
}
