using MauiHttpClientApp.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiHttpClientApp.ViewModels
{
    public class BaseViewModel<T> : INotifyPropertyChanged where T : class
    {

        bool isBusy = false;
        string title = string.Empty;


        public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>();

        public INavigationService<T> Navigation => DependencyService.Get<INavigationService<T>>();

        public bool IsBusy
        {
            get { return this.isBusy; }
            set { SetProperty(ref this.isBusy, value); }
        }

        public string Title
        {
            get { return this.title; }
            set { SetProperty(ref this.title, value); }
        }


        public event PropertyChangedEventHandler PropertyChanged;


        public virtual Task InitializeAsync(object parameter)
        {
            return Task.CompletedTask;
        }

        protected bool SetProperty<TProperty>(ref TProperty backingStore, TProperty value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<TProperty>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}