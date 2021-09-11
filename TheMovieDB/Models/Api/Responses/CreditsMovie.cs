using System.Collections.Generic;
using Newtonsoft.Json;

namespace TheMovieDB.Models.Api.Responses
{
    public class CreditsMovie
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("cast")]
        public List<Cast> Cast { get; set; }

        [JsonProperty("crew")]
        public List<Cast> Crew { get; set; }
    }
}
