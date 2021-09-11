using Newtonsoft.Json;

namespace TheMovieDB.Models.Api.Responses
{
    public class Cast
    {
        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("gender")]
        public long Gender { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("known_for_department")]
        public string KnownForDepartment { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("original_name")]
        public string OriginalName { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }

        [JsonProperty("cast_id")]
        public long? CastId { get; set; }

        [JsonProperty("character")]
        public string Character { get; set; }

        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("order")]
        public long? Order { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }
    }
}