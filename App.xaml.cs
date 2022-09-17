using MauiHttpClientApp.Services;
using MauiHttpClientApp.Views;

namespace MauiHttpClientApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        DependencyService.Register<ProductService>();
        //DependencyService.Register<NavigationService>();
        Routing.RegisterRoute(typeof(ProductPage).FullName, typeof(ProductPage));

        //MainPage = new AppShell();
        MainPage = new ProductPage();
	}
}
