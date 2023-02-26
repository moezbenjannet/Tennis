using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Tennis.Domain
{
    public class Country
    {
        [JsonPropertyName("picture")]
        public string Picture { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}
