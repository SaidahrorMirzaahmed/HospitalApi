using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HospitalApi.Service.Helpers;

public class EnumStringConverter : StringEnumConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }
}