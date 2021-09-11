using System;
namespace TheMovieDB.Tools.Exceptions
{
    public class NoInternetConnectionException : Exception
    {
        #region Public Properties

        public string StaticMessage { get; set; }

        #endregion


        #region Construtor

        public NoInternetConnectionException()
        {
        }

        #endregion
    }
}
