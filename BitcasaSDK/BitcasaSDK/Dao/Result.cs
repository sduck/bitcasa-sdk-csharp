using System.Collections.Generic;
using Newtonsoft.Json;

namespace BitcasaSdk.Dao
{
    public class Result
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        public List<Item> Items { get; set; } 
    }
}
