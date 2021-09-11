using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TheMovieDB.Models.Api.Responses
{
    public class Images
    {
        [JsonProperty("base_url")]
        public Uri BaseUrl { get; set; }

        [JsonProperty("secure_base_url")]
        public Uri SecureBaseUrl { get; set; }

        [JsonProperty("backdrop_sizes")]
        public List<string> BackdropSizes { get; set; }

        [JsonProperty("logo_sizes")]
        public List<string> LogoSizes { get; set; }

        [JsonProperty("poster_sizes")]
        public List<string> PosterSizes { get; set; }

        [JsonProperty("profile_sizes")]
        public List<string> ProfileSizes { get; set; }

        [JsonProperty("still_sizes")]
        public List<string> StillSizes { get; set; }
    }
}
