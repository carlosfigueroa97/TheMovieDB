using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using TheMovieDB.Droid.Renderers;
using TheMovieDB.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomFrameRenderer), typeof(DroidCustomFrameRenderer))]
namespace TheMovieDB.Droid.Renderers
{
    public class DroidCustomFrameRenderer : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        public DroidCustomFrameRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null && Control != null)
            {
                UpdateCornerRadius();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if(e.PropertyName == nameof(CustomFrameRenderer.CornerRadius)
                || e.PropertyName == nameof(CustomFrameRenderer))
            {
                UpdateCornerRadius();
            }
        }

        private void UpdateCornerRadius()
        {
            if(Control.Background is GradientDrawable gradientDrawable)
            {
                var cornerRadius = (Element as CustomFrameRenderer)?.CornerRadius;

                if (!cornerRadius.HasValue)
                {
                    return;
                }

                var topLeftCorner = Context.ToPixels(cornerRadius.Value.TopLeft);
                var topRightCorner = Context.ToPixels(cornerRadius.Value.TopRight);
                var bottomLeftCorner = Context.ToPixels(cornerRadius.Value.BottomLeft);
                var bottomRightCorner = Context.ToPixels(cornerRadius.Value.BottomRight);

                var radii = new[]
                {
                    topLeftCorner,
                    topLeftCorner,
                    topRightCorner,
                    topRightCorner,
                    bottomRightCorner,
                    bottomRightCorner,
                    bottomLeftCorner,
                    bottomLeftCorner
                };

                gradientDrawable.SetCornerRadii(radii);

                this.SetClipToOutline(false);
            }
        }
    }
}
