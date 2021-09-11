using System;
using TheMovieDB.Models.Api.Responses;

namespace TheMovieDB.Models.App
{
    public class CastWrapper
    {
        public Cast Cast { get; set; }

        public string Image { get; set; }
    }
}
