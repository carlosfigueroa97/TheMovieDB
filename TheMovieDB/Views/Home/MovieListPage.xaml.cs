using System.Linq;
using TheMovieDB.ViewModels.Home;
using Xamarin.Forms;

namespace TheMovieDB.Views.Home
{
    public partial class MovieListPage : ContentPage
    {
        MovieListPageViewModel vm => BindingContext as MovieListPageViewModel;

        public MovieListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(!vm.TopRatedList.Any()
                || !vm.UpCommingList.Any()
                || !vm.PopularList.Any())
            {
                vm.GetDataCommand.Execute(null);
            }
        }
    }
}
