using System;
using System.Diagnostics;
using System.Net;
using TheMovieDB.Services.Connection.Interfaces;
using TheMovieDB.Tools.Helpers;
using Xamarin.Essentials;

namespace TheMovieDB.Services.Connection.Implementations
{
    public class ConnectivityService : IConnectivityService
    {
        public ConnectivityService()
        {
        }

        public bool UserHasInternetConnection => Connectivity.NetworkAccess == NetworkAccess.Internet ? true : false;

        public bool CheckInternetAccess()
        {
            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(ConstantsGlobal.CheckInternetUrl);

                iNetRequest.Timeout = 5000;

                WebResponse iNetResponse = iNetRequest.GetResponse();

                iNetResponse.Close();

                return true;
            }
            catch (WebException ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false;
        }

        public bool CheckInternetAndConnectionAccess()
        {
            try
            {
                if (UserHasInternetConnection)
                {
                    if (CheckInternetAccess())
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
