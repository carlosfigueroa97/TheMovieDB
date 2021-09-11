using System.Collections.Generic;
using Newtonsoft.Json;

namespace TheMovieDB.Models.Api.Responses
{
    public class Configuration
    {
        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("change_keys")]
        public List<string> ChangeKeys { get; set; }
    }
}
