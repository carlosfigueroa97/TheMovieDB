using TheMovieDB.Models.Api.Responses;

namespace TheMovieDB.Models.App
{
    public class DetailWrapper
    {
        public DetailMovie DetailMovie { get; set; }

        public string Image { get; set; }

        public string Studio
        {
            get
            {
                string companies = string.Empty;

                foreach (var company in DetailMovie.ProductionCompanies)
                {
                    companies += company.Name + ",";
                }

                return companies;
            }
        }

        public string Genres
        {
            get
            {
                string genres = string.Empty;

                foreach (var genre in DetailMovie.Genres)
                {
                    genres += genre.Name + ",";
                }

                return genres;
            }
        }

        public string ReleaseYear => DetailMovie.ReleaseDate.ToString("yyyy");
    }
}
