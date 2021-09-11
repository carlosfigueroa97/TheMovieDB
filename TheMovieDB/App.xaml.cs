using TheMovieDB.Services.Api.Implementations;
using TheMovieDB.Services.Api.Interfaces;
using TheMovieDB.Services.Connection.Implementations;
using TheMovieDB.Services.Connection.Interfaces;
using TheMovieDB.Services.Crash.Implementations;
using TheMovieDB.Services.Crash.Interfaces;
using TheMovieDB.Services.Localization.Implementations;
using TheMovieDB.Services.Localization.Interfaces;
using TheMovieDB.Services.Navigation.Implementations;
using TheMovieDB.Services.Navigation.Interfaces;
using TheMovieDB.Utils.ServiceLocator;
using TheMovieDB.ViewModels.Detail;
using TheMovieDB.ViewModels.Home;
using TheMovieDB.Views.Detail;
using TheMovieDB.Views.Home;
using Xamarin.Forms;

namespace TheMovieDB
{
    public partial class App : Application
    {
        #region Private Properties

        private IServiceLocator _serviceLocator;
        private INavigationService _navigationService;

        private static bool _navigationInitialized;

        #endregion

        #region Constructor

        public App()
        {
            InitializeComponent();
            InitializeDependencyContainer();
            InitNavigation();
        }

        #endregion

        #region Protected Methods

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion

        #region Private Methods

        private void InitializeDependencyContainer()
        {
            _serviceLocator = ServiceLocatorProvider.Instance.Current;
            RegisterDependencies();
            RegisterViewModels();
            _serviceLocator.Init();
        }

        private void RegisterViewModels()
        {
            _serviceLocator.Register<MovieListPageViewModel>();
            _serviceLocator.Register<MovieDetailPageViewModel>();
        }

        private void RegisterDependencies()
        {
            _serviceLocator.RegisterSingle<ILocalizationService, LocalizationService>();
            _serviceLocator.RegisterSingle<IConnectivityService, ConnectivityService>();
            _serviceLocator.RegisterSingle<ICrashReporting, CrashReporting>();
            _serviceLocator.RegisterSingle<IApiClient, ApiClient>();
            _serviceLocator.RegisterSingle<IMovieService, MovieService>();
            _serviceLocator.RegisterSingle<INavigationService, NavigationService>();
        }

        private void BindViewsAndViewModels()
        {
            // Register all the view models with their corresponding pages
            _navigationService.RegisterViewMapping(typeof(MovieListPageViewModel), typeof(MovieListPage));
            _navigationService.RegisterViewMapping(typeof(MovieDetailPageViewModel), typeof(MovieDetailPage));
        }

        private void InitNavigation()
        {
            _navigationService = _navigationService ?? _serviceLocator.Resolve<INavigationService>();

            lock (typeof(App))
            {
                if (_navigationInitialized)
                    return;

                _navigationInitialized = true;
            }

            BindViewsAndViewModels();

            NavigationToFirstViewModel();
        }

        private void NavigationToFirstViewModel()
        {
            _navigationService.SetNewNavigationPage<MovieListPageViewModel>();
        }

        #endregion
    }
}
