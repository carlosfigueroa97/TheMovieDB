using Newtonsoft.Json;

namespace TheMovieDB.Models.Api.Responses
{
    public class SpokenLanguage
    {
        [JsonProperty("english_name")]
        public string EnglishName { get; set; }

        [JsonProperty("iso_639_1")]
        public string Iso639_1 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}