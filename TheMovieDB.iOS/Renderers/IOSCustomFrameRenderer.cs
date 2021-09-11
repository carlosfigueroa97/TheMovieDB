using System.ComponentModel;
using CoreAnimation;
using CoreGraphics;
using TheMovieDB.iOS.Renderers;
using TheMovieDB.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomFrameRenderer), typeof(IOSCustomFrameRenderer))]
namespace TheMovieDB.iOS.Renderers
{
    public class IOSCustomFrameRenderer : FrameRenderer
    {
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            UpdateCornerRadius();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(CustomFrameRenderer.CornerRadius)
                || e.PropertyName == nameof(CustomFrameRenderer))
            {
                UpdateCornerRadius();
            }
        }

        private double RetrieveCommonCornerRadius(CornerRadius cornerRadius)
        {
            var commonCornerRadius = cornerRadius.TopLeft;
            if(commonCornerRadius <= 0)
            {
                commonCornerRadius = cornerRadius.TopRight;
                if(commonCornerRadius <= 0)
                {
                    commonCornerRadius = cornerRadius.BottomLeft;
                    if(commonCornerRadius <= 0)
                    {
                        commonCornerRadius = cornerRadius.BottomRight;
                    }
                }
            }

            return commonCornerRadius;
        }

        private UIRectCorner RetrieveRoundedCorners(CornerRadius cornerRadius)
        {
            var roundedCorners = default(UIRectCorner);

            if(cornerRadius.TopLeft > 0)
            {
                roundedCorners |= UIRectCorner.TopLeft;
            }

            if(cornerRadius.TopRight > 0)
            {
                roundedCorners |= UIRectCorner.TopRight;
            }

            if(cornerRadius.BottomLeft > 0)
            {
                roundedCorners |= UIRectCorner.BottomLeft;
            }

            if(cornerRadius.BottomRight > 0)
            {
                roundedCorners |= UIRectCorner.BottomRight;
            }

            return roundedCorners;
        }

        private void UpdateCornerRadius()
        {
            var cornerRadius = (Element as CustomFrameRenderer)?.CornerRadius;
            if (!cornerRadius.HasValue)
            {
                return;
            }

            var roundedCornerRadius = RetrieveCommonCornerRadius(cornerRadius.Value);
            if(roundedCornerRadius <= 0)
            {
                return;
            }

            var roundedCorners = RetrieveRoundedCorners(cornerRadius.Value);

            var path = UIBezierPath.FromRoundedRect(Bounds, roundedCorners, new CGSize(roundedCornerRadius, roundedCornerRadius));
            var mask = new CAShapeLayer { Path = path.CGPath };
            NativeView.Layer.Mask = mask;
        }
    }
}
