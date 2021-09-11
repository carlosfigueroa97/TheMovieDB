using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TheMovieDB.Models.Api.Responses;
using TheMovieDB.Models.App;
using TheMovieDB.Services.Api.Interfaces;
using TheMovieDB.Services.Crash.Interfaces;
using TheMovieDB.Services.Navigation.Interfaces;
using TheMovieDB.ViewModels.Base;
using Xamarin.Forms;

namespace TheMovieDB.ViewModels.Detail
{
    public class MovieDetailPageViewModel : BaseViewModel<ResultWrapper>
    {
        #region Private Properties

        private ICrashReporting _crashReporting;
        private INavigationService _navigationService;
        private IMovieService _movieService;

        private ResultWrapper _parameter;
        private DetailWrapper _detailMovie;

        private bool _isEmpty;

        private string _baseUrlImage = string.Empty;

        #endregion

        #region Public Properties

        public Command GetDataCommand { get; set; }
        public Command GoBackCommand { get; set; }

        public ObservableCollection<CastWrapper> CastList { get; set; }

        public ResultWrapper Parameter
        {
            get => _parameter;
            set => SetProperty(ref _parameter, value);
        }

        public DetailWrapper DetailMovie
        {
            get => _detailMovie;
            set => SetProperty(ref _detailMovie, value);
        }

        public bool IsEmpty
        {
            get => _isEmpty;
            set => SetProperty(ref _isEmpty, value);
        }

        #endregion

        #region Constructor

        public MovieDetailPageViewModel(
            ICrashReporting crashReporting,
            INavigationService navigationService,
            IMovieService movieService)
        {
            _crashReporting = crashReporting;
            _navigationService = navigationService;
            _movieService = movieService;

            InitCommands();

            CastList = new ObservableCollection<CastWrapper>();
        }

        #endregion

        #region Init

        public override Task Init(ResultWrapper parameter)
        {
            Parameter = parameter;

            return base.Init(parameter);
        }

        private void InitCommands()
        {
            GetDataCommand = new Command(async () => await ExecuteGetDataCommand());
            GoBackCommand = new Command(async () => await ExecuteGoBackCommand());
        }

        #endregion

        #region Command Methods

        private async Task ExecuteGoBackCommand()
        {
            await _navigationService.GoBack();
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

                _baseUrlImage = await GetConfigurationAsync();

                List<Task> tasks = new List<Task>
                {
                    GetDetailMovieAsync(),
                    GetCreditsMovieAsync()
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

        private async Task GetDetailMovieAsync()
        {
            IsEmpty = false;

            var response = await _movieService.GetDetailMovieAsync(Parameter.Result.Id);
            if(response == null)
            {
                IsEmpty = true;
            }
            else
            {
                var model = new DetailWrapper
                {
                    DetailMovie = response,
                    Image = $"{_baseUrlImage}{response.PosterPath}"
                };
                DetailMovie = model;
                OnPropertyChanged(nameof(DetailMovie));
            }
        }

        private async Task GetCreditsMovieAsync()
        {
            IsEmpty = false;

            var response = await _movieService.GetCreditsMovieAsync(Parameter.Result.Id);
            if (response is null)
            {
                IsEmpty = true;
            }
            else
            {
                var list = response.Cast.Select(x => new CastWrapper
                {
                    Cast = x,
                    Image = $"{_baseUrlImage}{x.ProfilePath}"
                });
                CastList = new ObservableCollection<CastWrapper>(list);
            }

            OnPropertyChanged(nameof(CastList));
        }

        private async Task<string> GetConfigurationAsync()
        {
            var response = await _movieService.GetConfigurationMoviesAsync();

            if (response != null)
            {
                var baseUrl = response.Images.BaseUrl;
                var size = response.Images.BackdropSizes.First(x => x == "original");
                return $"{baseUrl}{size}";
            }

            return string.Empty;
        }

        #endregion
    }
}
