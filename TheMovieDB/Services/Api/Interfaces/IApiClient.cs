using System;
using System.Threading;
using System.Threading.Tasks;

namespace TheMovieDB.Services.Api.Interfaces
{
    public interface IApiClient
    {
        Task<string> GetAsync(string url, CancellationToken cancellationToken);
    }
}
