using System.Collections.Generic;
using Newtonsoft.Json;
using TheMovieDB.Models.Api.Base;

namespace TheMovieDB.Models.Api.Responses
{
    public class TopRated
    {
        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("total_pages")]
        public long TotalPages { get; set; }

        [JsonProperty("total_results")]
        public long TotalResults { get; set; }
    }
}
