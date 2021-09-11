using System;
namespace TheMovieDB.Services.Connection.Interfaces
{
    public interface IConnectivityService
    {
        bool UserHasInternetConnection { get; }

        bool CheckInternetAccess();

        bool CheckInternetAndConnectionAccess();
    }
}
