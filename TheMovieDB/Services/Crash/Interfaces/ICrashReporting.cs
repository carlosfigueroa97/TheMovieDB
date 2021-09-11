using System;
using System.Collections.Generic;
using TheMovieDB.Tools.Exceptions;

namespace TheMovieDB.Services.Crash.Interfaces
{
    public interface ICrashReporting
    {
        void TrackError(Exception ex, IDictionary<string, string> properties = null);

        void TrackApiError(ApiErrorException ex);

        void TrackNoInternetConnection(NoInternetConnectionException ex);
    }
}
