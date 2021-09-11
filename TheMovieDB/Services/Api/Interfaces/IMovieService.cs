using System.Threading;
using System.Threading.Tasks;
using TheMovieDB.Models.Api.Requests;
using TheMovieDB.Models.Api.Responses;

namespace TheMovieDB.Services.Api.Interfaces
{
    public interface IMovieService
    {
        Task<TopRated> GetTopRatedMoviesAsync(Params movieParams = null, CancellationToken cancellationToken = default);

        Task<UpComming> GetUpCommingMoviesAsync(Params movieParams = null, CancellationToken cancellationToken = default);

        Task<Popular> GetPopularMoviesAsync(Params movieParams = null, CancellationToken cancellationToken = default);

        Task<Configuration> GetConfigurationMoviesAsync(CancellationToken cancellationToken = default);

        Task<DetailMovie> GetDetailMovieAsync(long idmovie, Params movieParams = null, CancellationToken cancellationToken = default);

        Task<CreditsMovie> GetCreditsMovieAsync(long idmovie, Params movieParams = null, CancellationToken cancellationToken = default);
    }
}
