using System;
using Newtonsoft.Json;

namespace TheMovieDB.Models.Api.Responses
{
    public class Dates
    {
        [JsonProperty("maximum")]
        public DateTime Maximum { get; set; }

        [JsonProperty("minimum")]
        public DateTime Minimum { get; set; }
    }
}
