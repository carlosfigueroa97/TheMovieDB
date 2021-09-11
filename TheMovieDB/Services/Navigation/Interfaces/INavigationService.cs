using System;
using System.Threading.Tasks;
using TheMovieDB.ViewModels.Base;
using Xamarin.Forms;

namespace TheMovieDB.Services.Navigation.Interfaces
{
    public interface INavigationService
    {
        bool CanGoBack { get; }

        void ClearBackStack();

        Task GoBack();

        Page CurrentPage();

        void RegisterViewMapping(Type viewModel, Type view);

        Task SetNewNavigationPage<TVM>() where TVM : BaseVm;

        Task SetNewNavigationPage<TVM, TInitParameter>(TInitParameter parameter) where TVM : BaseViewModel<TInitParameter>;

        Task NavigateTo<TVM>() where TVM : BaseVm;

        Task NavigateTo<TVM, TInitParameter>(TInitParameter parameter) where TVM : BaseViewModel<TInitParameter>;

        Task PopToRootAsync(bool animated = true);
    }

    public interface INavigationAwaitable<T>
    {
        TaskCompletionSource<T> AwaitNavigationTask { get; set; }
    }
}
