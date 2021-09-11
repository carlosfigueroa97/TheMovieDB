using Newtonsoft.Json;

namespace TheMovieDB.Models.Api.Base
{
    public class BaseApiError
    {
        [JsonProperty("status_message")]
        public string StatusMessage { get; set; }

        [JsonProperty("status_code")]
        public string StatusCode { get; set; }
    }
}
