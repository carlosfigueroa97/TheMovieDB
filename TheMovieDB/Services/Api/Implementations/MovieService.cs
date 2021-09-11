using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheMovieDB.Models.Api.Requests;
using TheMovieDB.Models.Api.Responses;
using TheMovieDB.Services.Api.Interfaces;
using TheMovieDB.Services.Crash.Interfaces;
using TheMovieDB.Tools.Exceptions;
using TheMovieDB.Tools.Helpers;

namespace TheMovieDB.Services.Api.Implementations
{
    public class MovieService : IMovieService
    {
        #region Private Properties

        private readonly IApiClient _apiClient;
        private readonly ICrashReporting _crashReporting;

        #endregion

        #region Constructor

        public MovieService(
            IApiClient apiClient,
            ICrashReporting crashReporting)
        {
            _apiClient = apiClient;
            _crashReporting = crashReporting;
        }

        #endregion

        #region Public Methods

        public async Task<Configuration> GetConfigurationMoviesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _apiClient.GetAsync($"{ConstantsGlobal.ConfigurationUrl}api_key={ConstantsGlobal.ApiKey}", cancellationToken);

                if (response is null)
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<Configuration>(response);
            }
            catch (NoInternetConnectionException ex)
            {
                _crashReporting.TrackNoInternetConnection(ex);
                throw ex;
            }
            catch (ApiErrorException ex)
            {
                _crashReporting.TrackApiError(ex);
                return null;
            }
            catch (Exception ex)
            {
                _crashReporting.TrackError(ex);
                throw ex;
            }
        }

        public async Task<Popular> GetPopularMoviesAsync(Params movieParams = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _apiClient.GetAsync($"{ConstantsGlobal.PopularUrl}api_key={ConstantsGlobal.ApiKey}&" +
                    $"language={movieParams?.Language}&page={movieParams?.Page}", cancellationToken);

                if (response is null)
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<Popular>(response);
            }
            catch (NoInternetConnectionException ex)
            {
                _crashReporting.TrackNoInternetConnection(ex);
                throw ex;
            }
            catch (ApiErrorException ex)
            {
                _crashReporting.TrackApiError(ex);
                return null;
            }
            catch (Exception ex)
            {
                _crashReporting.TrackError(ex);
                throw ex;
            }
        }

        public async Task<TopRated> GetTopRatedMoviesAsync(Params movieParams = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _apiClient.GetAsync($"{ConstantsGlobal.TopRatedUrl}api_key={ConstantsGlobal.ApiKey}&" +
                    $"language={movieParams?.Language}&page={movieParams?.Page}", cancellationToken);

                if (response is null)
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<TopRated>(response);
            }
            catch (NoInternetConnectionException ex)
            {
                _crashReporting.TrackNoInternetConnection(ex);
                throw ex;
            }
            catch (ApiErrorException ex)
            {
                _crashReporting.TrackApiError(ex);
                return null;
            }
            catch (Exception ex)
            {
                _crashReporting.TrackError(ex);
                throw ex;
            }
        }

        public async Task<UpComming> GetUpCommingMoviesAsync(Params movieParams = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _apiClient.GetAsync($"{ConstantsGlobal.UpCommingUrl}api_key={ConstantsGlobal.ApiKey}&" +
                    $"language={movieParams?.Language}&page={movieParams?.Page}", cancellationToken);

                if (response is null)
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<UpComming>(response);
            }
            catch (NoInternetConnectionException ex)
            {
                _crashReporting.TrackNoInternetConnection(ex);
                throw ex;
            }
            catch (ApiErrorException ex)
            {
                _crashReporting.TrackApiError(ex);
                return null;
            }
            catch (Exception ex)
            {
                _crashReporting.TrackError(ex);
                throw ex;
            }
        }

        #endregion
    }
}
