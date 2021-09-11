using System;
using System.Net;

namespace TheMovieDB.Tools.Exceptions
{
    public class ApiErrorException : Exception
    {
        #region Public Properties

        public HttpStatusCode StatusCode { get; set; }

        public string StaticMessage { get; set; }

        #endregion


        #region Constructor

        public ApiErrorException()
        {
        }

        #endregion
    }
}
