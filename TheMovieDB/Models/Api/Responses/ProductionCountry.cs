using Newtonsoft.Json;

namespace TheMovieDB.Models.Api.Responses
{
    public class ProductionCountry
    {
        [JsonProperty("iso_3166_1")]
        public string Iso3166_1 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}