
using MauiHttpClientApp.ViewModels;

namespace MauiHttpClientApp.Services
{
    public interface INavigationService<T> where T : class
    {
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel<T>;

        Task NavigateToAsync<TViewModel>(bool isAbsoluteRoute) where TViewModel : BaseViewModel<T>;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel<T>;

        Task GoBackAsync();
    }
}