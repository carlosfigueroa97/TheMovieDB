using System;
namespace TheMovieDB.Services.Localization.Interfaces
{
    public interface ILocalizationService
    {
        string GetResource(string key);

        string GetTwoLetterISOLanguageName();
    }
}
