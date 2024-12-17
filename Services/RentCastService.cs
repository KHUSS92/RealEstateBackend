using RestSharp;

namespace RealEstateBackend.Services
{
    public class RentCastService : IRentCastService
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public RentCastService(IConfiguration configuration)
        {
            // Get API details from appsettings.json
            _apiKey = configuration["RentCast:ApiKey"] ?? throw new ArgumentNullException("RentCast API key not found.");
            _baseUrl = configuration["RentCast:BaseUrl"] ?? throw new ArgumentNullException("RentCast Base URL not found.");
        }

        public async Task<string> GetPropertyDetailsAsync(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Address cannot be null or empty.");

            string endpoint = $"{_baseUrl}properties/{Uri.EscapeDataString(address)}";

            var options = new RestClientOptions(endpoint);
            var client = new RestClient(options);
            var request = new RestRequest();
            request.AddHeader("accept", "application/json");
            request.AddHeader("X-Api-Key", _apiKey);


            var response = await client.GetAsync(request);
            
            ValidateResponse(response);

            return response.Content;
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
