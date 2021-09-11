﻿using System.Collections.Generic;
using Newtonsoft.Json;
using TheMovieDB.Models.Api.Base;

namespace TheMovieDB.Models.Api.Responses
{
    public class UpComming
    {
        [JsonProperty("dates")]
        public Dates Dates { get; set; }

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
