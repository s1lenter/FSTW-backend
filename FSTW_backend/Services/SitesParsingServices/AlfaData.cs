using System.Text.Json.Serialization;

namespace FSTW_backend.Services.SitesParsingServices
{
    public class AlfaData
    {
        [JsonPropertyName("data")]
        public List<AlfaInternship> Data { get; set; }
    }
    
    public class AlfaInternship
    {
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("externalId")]
        public int ExternalId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("direction")]
        public AlfaDirection Direction { get; set; }

        [JsonPropertyName("workFormat")]
        public AlfaWorkFormat WorkFormat { get; set; }

        [JsonIgnore]
        public string Requirements { get; set; }

        [JsonIgnore]
        public string Description { get; set; }

        [JsonIgnore]
        public string Link { get; set; }
    }

    public class AlfaDirection
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class AlfaWorkFormat
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class AlfaInternshipInfo
    {
        [JsonPropertyName("direction")]
        public AlfaFullDirection Direction { get; set; }

        [JsonPropertyName("requirements")]
        public string Requirements { get; set; }
    }

    public class AlfaFullDirection
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
