using System.Text.Json.Serialization;

namespace HealthClinicApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Title
    {
        Specialist = 1,
        Resident = 2,
        Nurse = 3

    }
}
