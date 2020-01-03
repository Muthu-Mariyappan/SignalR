using System.Collections.Generic;
using System.Text.Json;

namespace SignalRClient.Models
{
    public class MapPopupMessage
    {
        public string AccessToken { get; set; }
        public string Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Dictionary<string,string> Content { get; set; }

        public string Serialize(MapPopupMessage value)
        {
            return JsonSerializer.Serialize<MapPopupMessage>(value);
        }

        public MapPopupMessage Deserialize(string json)
        {
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true
            };

            return JsonSerializer.Deserialize<MapPopupMessage>(json, options);
        }
    }
}