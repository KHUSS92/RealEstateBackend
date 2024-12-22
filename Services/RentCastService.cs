using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstateBackend.Models;
using RestSharp;

namespace RealEstateBackend.Services
{
    public class RentCastService : IRentCastService
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly IConfiguration _config;

        public RentCastService(IConfiguration configuration)
        {
            // Get API details from appsettings.json
            _config = configuration;
            _apiKey = configuration["RentCast:ApiKey"] ?? throw new ArgumentNullException("RentCast API key not found.");
            _baseUrl = configuration["RentCast:BaseUrl"] ?? throw new ArgumentNullException("RentCast Base URL not found.");
        }

        [HttpGet("{propertyId}")]
        public async Task<Property?> GetPropertyByIdAsync(string propertyId)
        {
            if (string.IsNullOrWhiteSpace(propertyId))
                throw new ArgumentException("Address cannot be null or empty.");

            string endpoint = $"{_baseUrl}properties/{Uri.EscapeDataString(propertyId)}";

            var apiKey = _config["RentCastApiKey"];
            var options = new RestClientOptions(endpoint);
            var client = new RestClient(options);
            var request = new RestRequest();
            request.AddHeader("accept", "application/json");
            request.AddHeader("X-Api-Key", _apiKey);


            var response = await client.GetAsync(request);

            ValidateResponse(response);

            return JsonConvert.DeserializeObject<Property>(response.Content);
        }

        [HttpGet("{propertyId}/history")]
        public async Task<List<PropertyHistory>> GetPropertyHistoryAsync(string propertyId)
        {
            if (string.IsNullOrWhiteSpace(propertyId))
                throw new ArgumentException("Address cannot be null or empty.");

            string endpoint = $"{_baseUrl}properties/{Uri.EscapeDataString(propertyId)}";

            var apiKey = _config["RentCastApiKey"];
            var options = new RestClientOptions(endpoint);
            var client = new RestClient(options);
            var request = new RestRequest();
            request.AddHeader("accept", "application/json");
            request.AddHeader("X-Api-Key", _apiKey);

            var response = await client.GetAsync(request);

            ValidateResponse(response);

            try
            {
                var history = JsonConvert.DeserializeObject<List<PropertyHistory>>(response.Content) ?? throw new ApplicationException("Deserialization returned null. Please verify the API response format.");
                return history;
            }
            catch (JsonException jsonEx)
            {
                throw new JsonException($"JSON deserialization error: {jsonEx.Message}");
            }
        }

        [HttpGet("{propertyId}/address")]
        public async Task<PropertyAddress?> GetPropertyAddressAsync(string propertyId)
        {
            if (string.IsNullOrWhiteSpace(propertyId))
                throw new ArgumentException("Address cannot be null or empty.");

            string endpoint = $"{_baseUrl}properties/{Uri.EscapeDataString(propertyId)}";

            var options = new RestClientOptions(endpoint);
            var client = new RestClient(options);
            var request = new RestRequest();
            request.AddHeader("accept", "application/json");
            request.AddHeader("X-Api-Key", _apiKey);


            var response = await client.GetAsync(request);
            
            ValidateResponse(response);

            try
            {
                var propAddress = JsonConvert.DeserializeObject<PropertyAddress?>(response.Content) ?? throw new ApplicationException("Deserialization returned null. Please verify the API response format.");
                return propAddress;
            }
            catch (JsonException jsonEx)
            {
                throw new JsonException($"JSON deserialization error: {jsonEx.Message}");
            }
        }

        //Maintain Seperation of Concerns
        private void ValidateResponse(RestResponse response) {

            if (response is null)
                throw new ApplicationException("No response from server.");

            if (!response.IsSuccessful)
                throw new ApplicationException($"Error fetching property details: {response.ErrorMessage ?? "Unknown error."}");

            if (string.IsNullOrWhiteSpace(response.Content))
                throw new ApplicationException("The API returned an empty response.");
        }
    }
}
