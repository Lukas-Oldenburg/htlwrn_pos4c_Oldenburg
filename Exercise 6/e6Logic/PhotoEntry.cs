using System;
using System.Text.Json.Serialization;

namespace e6Logic
{
    public class PhotoEntry
    {

        [JsonPropertyName("pic")]
        public required string Pic { get; set; }

        [JsonPropertyName("takenBy")]
        public required string TakenBy { get; set; }

        [JsonPropertyName("uploadYear")]
        public int UploadYear { get; set; }
    }
}
