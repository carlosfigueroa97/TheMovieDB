using Xamarin.Forms;

namespace TheMovieDB.Controls
{
    public partial class Rating : ContentView
    {
        #region Private Properties

        private double opacity = 0.3;

        #endregion

        #region Public Properties

        public static readonly BindableProperty RatingStarProperty = BindableProperty.Create(
            propertyName: nameof(RatingStar),
            returnType: typeof(double),
            declaringType: typeof(double),
            defaultValue: 10.0,
            propertyChanged: OnRatingStar);

        public double RatingStar
        {
            get => (double)GetValue(RatingStarProperty);
            set => SetValue(RatingStarProperty, value);
        }

        #endregion

        public Rating()
        {
            InitializeComponent();
        }

        #region Private Methods

        private static void OnRatingStar(BindableObject bindable, object oldVal, object newVal)
        {
            var newBindable = bindable as Rating;
            var starOne = newBindable.starOne;
            var starTwo = newBindable.starTwo;
            var starThree = newBindable.starThree;
            var starFourth = newBindable.starFourth;
            var starFive = newBindable.starFive;
            var rating = newBindable.RatingStar;
            var opacity = newBindable.opacity;

            if(rating <= 0)
            {
                starOne.Opacity = opacity;
                starTwo.Opacity = opacity;
                starThree.Opacity = opacity;
                starFourth.Opacity = opacity;
                starFive.Opacity = opacity;
            }
            else if (rating >= 1 && rating <= 2)
            {
                starTwo.Opacity = opacity;
                starThree.Opacity = opacity;
                starFourth.Opacity = opacity;
                starFive.Opacity = opacity;
            }
            else if (rating >= 2.1 && rating <= 4)
            {
                starThree.Opacity = opacity;
                starFourth.Opacity = opacity;
                starFive.Opacity = opacity;
            }
            else if (rating >= 4.1 && rating <= 6)
            {
                starFourth.Opacity = opacity;
                starFive.Opacity = opacity;
            }
            else if (rating >= 6.1 && rating <= 8)
            {
                starFive.Opacity = opacity;
            }
        }

        #endregion
    }
}
