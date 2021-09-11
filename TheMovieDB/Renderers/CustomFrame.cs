using System;
using Xamarin.Forms;

namespace TheMovieDB.Renderers
{
    public class CustomFrameRenderer : Frame
    {
        public static new readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CustomFrameRenderer),
            typeof(CornerRadius),
            typeof(CustomFrameRenderer));

        public new CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public CustomFrameRenderer()
        {
            base.CornerRadius = 0;
        }
    }
}
