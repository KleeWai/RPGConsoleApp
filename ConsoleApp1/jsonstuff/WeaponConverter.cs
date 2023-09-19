using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using RPG.weapons;

namespace RPG.jsonstuff
{
    public class WeaponConverter : JsonConverter<weapons.Weapon>
    {
        public override weapons.Weapon Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                int weaponType = root.GetProperty("type").GetInt32();

                switch (weaponType)
                {
                    case 1: // Replace with the actual type identifier for your weapons
                        return JsonSerializer.Deserialize<Hand>(root.GetRawText());
                    case 2: // Replace with the actual type identifier for your weapons
                        return JsonSerializer.Deserialize<Hand>(root.GetRawText());
                    // Add cases for other weapon types
                    default:
                        return JsonSerializer.Deserialize<Hand>(root.GetRawText());
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, weapons.Weapon value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
