using TheMovieDB.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TheMovieDB.Renderers.EntryRenderer), typeof(IOSEntryRenderer))]
namespace TheMovieDB.iOS.Renderers
{
    public class IOSEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null)
            {
                if (Control != null)
                {
                    Control.BorderStyle = UIKit.UITextBorderStyle.None;
                }
            }
        }

        public IOSEntryRenderer()
        {
        }
    }
}
