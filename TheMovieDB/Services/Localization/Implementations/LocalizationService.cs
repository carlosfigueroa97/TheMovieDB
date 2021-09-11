using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
using TheMovieDB.Services.Localization.Interfaces;
using TheMovieDB.Tools.Helpers;

namespace TheMovieDB.Services.Localization.Implementations
{
    public class LocalizationService : ILocalizationService
    {
        static readonly Lazy<ResourceManager> resmgr = new Lazy<ResourceManager>(() =>
            new ResourceManager(
                ConstantsGlobal.ResourcesPath,
                typeof(LocalizationService).GetTypeInfo().Assembly)
            );

        public LocalizationService()
        {
        }

        public string GetResource(string key)
        {
            var ci = Thread.CurrentThread.CurrentUICulture;
            string translation = key;

            try
            {
                translation = resmgr.Value.GetString(key, ci);

                if (translation == null)
                    translation = key;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LocalizationService: GetResource Exception {ex.Message}");
            }

            return translation;
        }

        public string GetTwoLetterISOLanguageName()
        {
            var currentCulture = CultureInfo.InstalledUICulture;
            return currentCulture.TwoLetterISOLanguageName;
        }
    }
}
