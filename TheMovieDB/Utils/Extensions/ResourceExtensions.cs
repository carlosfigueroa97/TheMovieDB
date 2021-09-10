using System;
using Xamarin.Forms;

namespace TheMovieDB.Utils.Extensions
{
    public static class ResourceExtensions
    {
        public static object GetResourceValue(string keyName)
        {
            Application.Current.Resources.TryGetValue(keyName, out var retVal);
            return retVal;
        }
    }
}
