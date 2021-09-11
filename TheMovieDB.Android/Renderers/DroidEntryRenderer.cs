using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;
using TheMovieDB.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TheMovieDB.Renderers.EntryRenderer), typeof(DroidEntryRenderer))]
namespace TheMovieDB.Droid.Renderers
{
    public class DroidEntryRenderer : EntryRenderer
    {
        public DroidEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if(Control != null)
                {
                    GradientDrawable gradientBackground = new GradientDrawable();

                    gradientBackground.SetColor(Android.Graphics.Color.Transparent);
                    Control.SetBackground(gradientBackground);
                }
            }
        }
        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}
