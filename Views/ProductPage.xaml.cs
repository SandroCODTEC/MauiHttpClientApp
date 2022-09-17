using MauiHttpClientApp.ViewModels;

namespace MauiHttpClientApp.Views;

public partial class ProductPage : ContentPage
{
	public ProductPage()
	{
		InitializeComponent();
        BindingContext = ViewModel = new ProductsViewModel();
        ViewModel.OnAppearing();
    }
    ProductsViewModel ViewModel { get; }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.OnAppearing();
    }
}