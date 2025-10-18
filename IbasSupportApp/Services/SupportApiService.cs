using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using IbasSupportApp.Models;

namespace IbasSupportApp.Services
{
    public class SupportApiService
    {
        // 🔧 Ret denne URL til din API-port (5235 i dit tilfælde)
        private readonly string _apiBaseUrl = "http://localhost:5235";

        private readonly HttpClient _httpClient;

        public SupportApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendSupportMessageAsync(SupportMessage message)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/support", message);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
