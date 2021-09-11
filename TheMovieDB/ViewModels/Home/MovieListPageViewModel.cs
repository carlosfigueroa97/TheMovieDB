using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TheMovieDB.Models.App;
using TheMovieDB.Services.Api.Interfaces;
using TheMovieDB.Services.Crash.Interfaces;
using TheMovieDB.Services.Navigation.Interfaces;
using TheMovieDB.ViewModels.Base;
using TheMovieDB.ViewModels.Detail;
using Xamarin.Forms;

namespace TheMovieDB.ViewModels.Home
{
    public class MovieListPageViewModel : BaseVm
    {
        #region Private Properties

        private readonly ICrashReporting _crashReporting;
        private readonly IMovieService _movieService;
        private readonly INavigationService _navigationService;

        private string _baseUrl = string.Empty;
        private string _searchText;

        private ResultWrapper _topRateSelected;
        private ResultWrapper _upCommingSelected;
        private ResultWrapper _popularSelected;

        #endregion

        #region Public Properties

        public Command GetDataCommand { get; set; }
        public Command TopRatedSelectedCommand { get; set; }
        public Command UpCommingSelectedCommand { get; set; }
        public Command PopularSelectedCommand { get; set; }
        public Command SearchCommand { get; set; }

        public ObservableCollection<ResultWrapper> TopRatedList { get; set; }
        public ObservableCollection<ResultWrapper> UpCommingList { get; set; }
        public ObservableCollection<ResultWrapper> PopularList { get; set; }

        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public ResultWrapper TopRateSelected
        {
            get => _topRateSelected;
            set => SetProperty(ref _topRateSelected, value);
        }

        public ResultWrapper UpCommingSelected
        {
            get => _upCommingSelected;
            set => SetProperty(ref _upCommingSelected, value);
        }

        public ResultWrapper PopularSelected
        {
            get => _popularSelected;
            set => SetProperty(ref _popularSelected, value);
        }

        #endregion

        #region Constructor

        public MovieListPageViewModel(
            ICrashReporting crashReporting,
            IMovieService movieService,
            INavigationService navigationService)
        {
            _crashReporting = crashReporting;
            _movieService = movieService;
            _navigationService = navigationService;

            InitCommands();
        }

        #endregion

        #region Command Methods

        private async Task ExecuteGetDataCommand()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                _baseUrl = await GetConfigurationAsync();

                Task[] tasks = new Task[]
                {
                    GetTopRatedAsync(),
                    GetUpCommingAsync(),
                    GetPopularAsync()
                };

                await Task.WhenAll(tasks);

            }
            catch (Exception ex)
            {
                _crashReporting.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        #region Private Methods

        private void InitCommands()
        {
            GetDataCommand = new Command(async () => await ExecuteGetDataCommand());
            TopRatedSelectedCommand = new Command(async () => await ExecuteTopRatedSelectedCommand());
            UpCommingSelectedCommand = new Command(async () => await ExecuteUpCommingSelectedCommand());
            PopularSelectedCommand = new Command(async () => await ExecutePopularSelectedCommand());
            SearchCommand = new Command(async () => await ExecuteSearchCommand());
        }

        private Task ExecuteSearchCommand()
        {
            throw new NotImplementedException();
        }

        private async Task ExecutePopularSelectedCommand()
        {
            if(PopularSelected is null)
            {
                return;
            }

            await _navigationService.NavigateTo<MovieDetailPageViewModel, ResultWrapper>(PopularSelected);
        }

        private async Task ExecuteUpCommingSelectedCommand()
        {
            if (UpCommingSelected is null)
            {
                return;
            }

            await _navigationService.NavigateTo<MovieDetailPageViewModel, ResultWrapper>(UpCommingSelected);
        }

        private async Task ExecuteTopRatedSelectedCommand()
        {
            if (TopRateSelected is null)
            {
                return;
            }

            await _navigationService.NavigateTo<MovieDetailPageViewModel, ResultWrapper>(TopRateSelected);
        }

        private async Task<string> GetConfigurationAsync()
        {
            var response = await _movieService.GetConfigurationMoviesAsync();

            if(response != null)
            {
                var baseUrl = response.Images.BaseUrl;
                var size = response.Images.BackdropSizes.First(x => x == "original");
                return $"{baseUrl}{size}";
            }

            return string.Empty;
        }

        private async Task GetTopRatedAsync()
        {
            var response = await _movieService.GetTopRatedMoviesAsync();

            if(response is null
                || (bool)!response?.Results?.Any())
            {
                TopRatedList = new ObservableCollection<ResultWrapper>();
            }
            else
            {
                var topRatedList = response.Results.Skip(10);
                var list = topRatedList.Select(x => new ResultWrapper
                {
                    Model = x,
                    Image = $"_baseUrl{x.BackdropPath}"
                });
                TopRatedList = new ObservableCollection<ResultWrapper>(list);
            }

            OnPropertyChanged(nameof(TopRatedList));
        }

        private async Task GetUpCommingAsync()
        {
            var response = await _movieService.GetUpCommingMoviesAsync();

            if (response is null
                || (bool)!response?.Results?.Any())
            {
                UpCommingList = new ObservableCollection<ResultWrapper>();
            }
            else
            {
                var upCommmingList = response.Results.Skip(10);
                var list = upCommmingList.Select(x => new ResultWrapper
                {
                    Model = x,
                    Image = $"_baseUrl{x.BackdropPath}"
                });
                UpCommingList = new ObservableCollection<ResultWrapper>(list);
            }

            OnPropertyChanged(nameof(UpCommingList));
        }

        private async Task GetPopularAsync()
        {
            var response = await _movieService.GetPopularMoviesAsync();

            if (response is null
                || (bool)!response?.Results?.Any())
            {
                PopularList = new ObservableCollection<ResultWrapper>();
            }
            else
            {
                var popularList = response.Results.Skip(10);
                var list = popularList.Select(x => new ResultWrapper
                {
                    Model = x,
                    Image = $"_baseUrl{x.BackdropPath}"
                });
                PopularList = new ObservableCollection<ResultWrapper>(list);
            }

            OnPropertyChanged(nameof(PopularList));
        }

        #endregion
    }
}
