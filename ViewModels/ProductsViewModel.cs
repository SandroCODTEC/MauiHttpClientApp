using MauiHttpClientApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiHttpClientApp.ViewModels
{
    public class ProductsViewModel: BaseViewModel<Product>
    {
        Product _selectedItem;

        public ProductsViewModel()
        {
            Title = "Products";
            Items = new ObservableCollection<Product>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Product>(OnItemSelected);
            AddItemCommand = new Command(OnAddItem);
        }


        public ObservableCollection<Product> Items { get; }

        public Command LoadItemsCommand { get; }

        public Command AddItemCommand { get; }

        public Command<Product> ItemTapped { get; }

        public Product SelectedItem
        {
            get => this._selectedItem;
            set
            {
                SetProperty(ref this._selectedItem, value);
                OnItemSelected(value);
            }
        }


        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
            ExecuteLoadItemsCommand().Wait();
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnAddItem(object obj)
        {
            //throw new NotImplementedException();
        }

        async void OnItemSelected(Product item)
        {
            //throw new NotImplementedException();

        }
    }
}
