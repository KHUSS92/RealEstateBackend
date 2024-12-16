using RealEstateBackend.Models;
using Newtonsoft.Json;

namespace RealEstateBackend.Services
{
    public class ZillowService
    {
        private readonly HttpClient _httpClient;

        public ZillowService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ZillowApi");
        }

        public async Task<Property?> GetPropertyById(string propertyId)
        {
            // Construct the full URL relative to the BaseAddress
            var response = await _httpClient.GetAsync($"properties/{propertyId}");

            if (!response.IsSuccessStatusCode)
            {
                // Handle errors (e.g., log, throw exception, return null, etc.)
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Property>(content);
        }
    }

}
