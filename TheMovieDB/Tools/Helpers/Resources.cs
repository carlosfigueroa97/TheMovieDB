using TheMovieDB.Services.Localization.Interfaces;
using TheMovieDB.Utils.ServiceLocator;

namespace TheMovieDB.Tools.Helpers
{
    public static class Resources
    {
        #region Private Properties

        private static ILocalizationService _localizationService =>
               ServiceLocatorProvider
               .Instance
               .GetService<ILocalizationService>();

        #endregion

        #region Public Properties

        public static string NoInternetConnection = GetResources("NoInternetConnection");
        public static string UnexpectedExceptionOcurred = GetResources("UnexpectedExceptionOcurred");

        #endregion

        #region Private Methods

        private static string GetResources(string key)
        {
            return _localizationService.GetResource(key);
        }

        #endregion
    }
}
