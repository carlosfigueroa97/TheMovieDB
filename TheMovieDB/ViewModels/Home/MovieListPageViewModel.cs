using System;
using System.Collections.Generic;
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

        private bool _lookingFor;

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
        public ObservableCollection<ResultWrapper> TopRatedListCopy { get; set; }
        public ObservableCollection<ResultWrapper> UpCommingListCopy { get; set; }
        public ObservableCollection<ResultWrapper> PopularListCopy { get; set; }

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

            TopRatedList = new ObservableCollection<ResultWrapper>();
            UpCommingList = new ObservableCollection<ResultWrapper>();
            PopularList = new ObservableCollection<ResultWrapper>();
        }

        #endregion

        #region Command Methods

        private void ExecuteSearchCommand(string searchText)
        {
            if (_lookingFor)
            {
                return;
            }

            _lookingFor = true;

            if (string.IsNullOrEmpty(searchText))
            {
                TopRatedList = TopRatedListCopy;
                UpCommingList = UpCommingListCopy;
                PopularList = PopularListCopy;
            }
            else
            {
                if(searchText.Length > 2)
                {
                    if ((bool)(TopRatedListCopy?.Any()))
                    {
                        var topRatedFiltered = TopRatedListCopy
                                                .Where(x =>
                                                        x.Result.Title
                                                        .ToLower()
                                                        .Contains(searchText.ToLower()))
                                                .ToList();
                        TopRatedList = new ObservableCollection<ResultWrapper>(topRatedFiltered);
                    }

                    if ((bool)(UpCommingListCopy?.Any()))
                    {
                        var upCommingFiltered = UpCommingListCopy
                                                .Where(x =>
                                                        x.Result.Title
                                                        .ToLower()
                                                        .Contains(searchText.ToLower()))
                                                .ToList();
                        UpCommingList = new ObservableCollection<ResultWrapper>(upCommingFiltered);
                    }


                    if ((bool)(PopularListCopy?.Any()))
                    {
                        var popularFiltered = PopularListCopy
                                                .Where(x =>
                                                        x.Result.Title
                                                        .ToLower()
                                                        .Contains(searchText.ToLower()))
                                                .ToList();
                        PopularList = new ObservableCollection<ResultWrapper>(popularFiltered);
                    }
                }
            }

            OnPropertyChanged(nameof(TopRatedList));
            OnPropertyChanged(nameof(UpCommingList));
            OnPropertyChanged(nameof(PopularList));

            _lookingFor = false;
        }

        private async Task ExecutePopularSelectedCommand()
        {
            if (PopularSelected is null)
            {
                return;
            }

            await _navigationService.NavigateTo<MovieDetailPageViewModel, ResultWrapper>(PopularSelected);

            PopularSelected = null;
        }

        private async Task ExecuteUpCommingSelectedCommand()
        {
            if (UpCommingSelected is null)
            {
                return;
            }

            await _navigationService.NavigateTo<MovieDetailPageViewModel, ResultWrapper>(UpCommingSelected);

            UpCommingSelected = null;
        }

        private async Task ExecuteTopRatedSelectedCommand()
        {
            if (TopRateSelected is null)
            {
                return;
            }

            await _navigationService.NavigateTo<MovieDetailPageViewModel, ResultWrapper>(TopRateSelected);

            TopRateSelected = null;
        }

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

                List<Task> tasks = new List<Task>
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
            SearchCommand = new Command<string>((searchText) => ExecuteSearchCommand(searchText));
        }

        private async Task<string> GetConfigurationAsync()
        {
            var response = await _movieService.GetConfigurationMoviesAsync();

            if(response != null)
            {
                var baseUrl = response.Images.SecureBaseUrl;
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
                TopRatedListCopy = new ObservableCollection<ResultWrapper>();
            }
            else
            {
                var topRatedList = response.Results.Skip(10);
                var list = topRatedList.Select(x => new ResultWrapper
                {
                    Result = x,
                    Image = $"{_baseUrl}{x.BackdropPath}"
                });
                TopRatedList = new ObservableCollection<ResultWrapper>(list);
                TopRatedListCopy = new ObservableCollection<ResultWrapper>(list);
            }

            OnPropertyChanged(nameof(TopRatedList));
        }

        private async Task GetUpCommingAsync()
        {
            var response = await _movieService.GetUpCommingMoviesAsync();

            if (response is null || !response.Results.Any())
            {
                UpCommingList = new ObservableCollection<ResultWrapper>();
                UpCommingListCopy = new ObservableCollection<ResultWrapper>();
            }
            else
            {
                var upCommmingList = response.Results.Skip(10);
                var list = upCommmingList.Select(x => new ResultWrapper
                {
                    Result = x,
                    Image = $"{_baseUrl}{x.BackdropPath}"
                });
                UpCommingList = new ObservableCollection<ResultWrapper>(list);
                UpCommingListCopy = new ObservableCollection<ResultWrapper>(list);
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
                PopularListCopy = new ObservableCollection<ResultWrapper>();
            }
            else
            {
                var popularList = response.Results.Skip(10);
                var list = popularList.Select(x => new ResultWrapper
                {
                    Result = x,
                    Image = $"{_baseUrl}{x.BackdropPath}"
                });
                PopularList = new ObservableCollection<ResultWrapper>(list);
                PopularListCopy = new ObservableCollection<ResultWrapper>(list);
            }

            OnPropertyChanged(nameof(PopularList));
        }

        #endregion
    }
}
