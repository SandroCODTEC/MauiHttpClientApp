using MauiHttpClientApp.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MauiHttpClientApp.Services
{
    public class ProductService : IDataStore<Product>
    {
#if __ANDROID__
        //private static readonly HttpClient HttpClient = new(new HttpsClientHandlerService().GetPlatformMessageHandler());
        private static readonly HttpClient HttpClient = new();
#else
        private static readonly HttpClient HttpClient = new();
#endif
        private const string APIUrl = "https://js.devexpress.com/Demos/DevAV/";
        private const string PostEndPointUrl = APIUrl + "odata/Products";
        private const string ApplicationJson = "application/json";
        public ProductService()
        {
            HttpClient.Timeout = TimeSpan.FromSeconds(15);
        }
        public async Task<bool> AddItemAsync(Product item)
        {
            try
            {
                var httpResponseMessage = await HttpClient.PostAsync(PostEndPointUrl,
                    new StringContent(JsonSerializer.Serialize(item), Encoding.UTF8, ApplicationJson));

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    await Shell.Current.DisplayAlert("Error", await httpResponseMessage.Content.ReadAsStringAsync(), "OK");
                }
                return httpResponseMessage.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                return false;
            }
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetItemAsync(string id)
            => (await RequestItemAsync($"?$filter={nameof(Product.Product_ID)} eq {id}")).FirstOrDefault();

        public async Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false)
        {
            return await RequestItemAsync();
        }

        public static async Task<IEnumerable<Product>> RequestItemAsync(string query = null)
        {
            try
            {
                Uri uri = new Uri($"{PostEndPointUrl}{query}");

                var json = await HttpClient.GetStringAsync(uri);
                return JsonNode.Parse(json)["value"].Deserialize<IEnumerable<Product>>();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                return null;
            }
        }

        public Task<bool> UpdateItemAsync(Product item)
        {
            throw new NotImplementedException();
        }
    }
}