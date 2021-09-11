using TheMovieDB.ViewModels.Detail;
using Xamarin.Forms;

namespace TheMovieDB.Views.Detail
{
    public partial class MovieDetailPage : ContentPage
    {
        MovieDetailPageViewModel vm => BindingContext as MovieDetailPageViewModel;
        public MovieDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            vm.GetDataCommand.Execute(null);
        }
    }
}
