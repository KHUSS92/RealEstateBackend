using RealEstateBackend.Models;

namespace RealEstateBackend.Services
{
    public interface IRentCastService
    {
        Task<Property?> GetPropertyByIdAsync(string propertyId);
        Task<List<PropertyHistory>> GetPropertyHistoryAsync(string propertyId);
        Task<PropertyAddress?> GetPropertyAddressAsync(string propertyId);
    }
}
