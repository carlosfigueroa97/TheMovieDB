using System;
using TheMovieDB.Utils.Extensions;
using Xamarin.Forms;

namespace TheMovieDB.Tools.Helpers
{
    public static class Colors
    {
        public static Color PrimaryBackgroundColor = (Color)ResourceExtensions.GetResourceValue(nameof(PrimaryBackgroundColor));
        public static Color SecondBackgroundColor = (Color)ResourceExtensions.GetResourceValue(nameof(SecondBackgroundColor));
        public static Color SearchBarBackgroundColor = (Color)ResourceExtensions.GetResourceValue(nameof(SearchBarBackgroundColor));
        public static Color WhiteTextColor = (Color)ResourceExtensions.GetResourceValue(nameof(WhiteTextColor));
        public static Color LightGrayTextColor = (Color)ResourceExtensions.GetResourceValue(nameof(LightGrayTextColor));
        public static Color GrayTextColor = (Color)ResourceExtensions.GetResourceValue(nameof(GrayTextColor));
        public static Color RaitingColor = (Color)ResourceExtensions.GetResourceValue(nameof(RaitingColor));
        public static Color GeneralColor = (Color)ResourceExtensions.GetResourceValue(nameof(GeneralColor));
    }
}
