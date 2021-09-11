namespace TheMovieDB.Tools.Helpers
{
    public static class ConstantsGlobal
    {
        public const string ResourcesPath = "TheMovieDB.Localization.AppResources";
        public const string ApiKey = "e6cdb76ea3b4cd457e92e4935446fee2";
        public const string ApiUrl = "https://api.themoviedb.org/3/";
        public const string CheckInternetUrl = "https://google.com";
        public const string TopRatedUrl = ApiUrl + "movie/top_rated?";
        public const string UpCommingUrl = ApiUrl + "movie/upcoming?";
        public const string PopularUrl = ApiUrl + "movie/popular?";
        public const string ConfigurationUrl = ApiUrl + "configuration?";
        public const string DetailMovie = ApiUrl + "movie/";
    }
}
