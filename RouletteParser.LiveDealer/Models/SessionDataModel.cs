using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RouletteParser.LiveDealer.Models
{
    public class SessionDataModel
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }
        [JsonProperty("session_id")]
        public string SessionId { get; set; }
    }
}
