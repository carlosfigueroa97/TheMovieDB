using System;
using System.Collections.Generic;
using System.Diagnostics;
using TheMovieDB.Services.Crash.Interfaces;
using TheMovieDB.Tools.Exceptions;

namespace TheMovieDB.Services.Crash.Implementations
{
    public class CrashReporting : ICrashReporting
    {
        public CrashReporting()
        {
        }

        #region Methods

        public void TrackError(Exception ex, IDictionary<string, string> properties = null)
        {
#if DEBUG
            Debug.WriteLine($"------- GENERAL ERROR -------");
            Debug.WriteLine($"Track error message {ex}");
#endif
        }

        public void TrackApiError(ApiErrorException ex)
        {
#if DEBUG
            Debug.WriteLine($"------- API ERROR EXCEPTION -------");
            Debug.WriteLine($"Message {ex.Message}");
            Debug.WriteLine($"Status Code {ex.StatusCode}");
            Debug.WriteLine($"Source {ex.Source}");
#endif
        }

        public void TrackNoInternetConnection(NoInternetConnectionException ex)
        {
#if DEBUG
            Debug.WriteLine($"------- NO INTERNET CONNECTION -------");
            Debug.WriteLine($"Message {ex.StaticMessage}");
#endif
        }

        #endregion
    }
}
