using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TheMovieDB.Services.Api.Interfaces;
using TheMovieDB.Services.Connection.Interfaces;
using TheMovieDB.Services.Crash.Interfaces;
using TheMovieDB.Tools.Exceptions;
using TheMovieDB.Tools.Helpers;

namespace TheMovieDB.Services.Api.Implementations
{
    public class ApiClient : IApiClient
    {
        #region Private Properties

        private readonly ICrashReporting _crashReporting;
        private readonly IConnectivityService _connectivityService;

        private static HttpClient _httpClient;
        protected HttpClient HttpClient
        {
            get
            {
                if(_httpClient == null)
                {
                    _httpClient = new HttpClient
                    {
                        BaseAddress = new Uri(ConstantsGlobal.ApiUrl),
                        Timeout = TimeSpan.FromSeconds(10)
                    };
                }

                return _httpClient;
            }
        }

        #endregion

        #region Constructor

        public ApiClient(
            ICrashReporting crashReporting,
            IConnectivityService connectivityService)
        {
            _crashReporting = crashReporting;
            _connectivityService = connectivityService;
        }

        #endregion

        #region Public Methods

        public async Task<string> GetAsync(string url, CancellationToken cancellationToken)
        {
            HttpResponseMessage httpResponseMessage = null;

            if (!_connectivityService.UserHasInternetConnection)
            {
                throw new NoInternetConnectionException
                {
                    StaticMessage = Resources.NoInternetConnection
                };
            }

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                httpResponseMessage = await HttpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);

                cancellationToken.ThrowIfCancellationRequested();

                var readString = await httpResponseMessage.Content.ReadAsStringAsync();

#if DEBUG
                Debug.WriteLine(readString);
#endif

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return readString;
                }
            }
            catch (OperationCanceledException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _crashReporting.TrackError(ex);
                throw ex;
            }

            throw new ApiErrorException
            {
                StatusCode = httpResponseMessage == null ? System.Net.HttpStatusCode.Ambiguous : httpResponseMessage.StatusCode,
                StaticMessage = Resources.UnexpectedExceptionOcurred
            };
        }

        #endregion
    }
}
