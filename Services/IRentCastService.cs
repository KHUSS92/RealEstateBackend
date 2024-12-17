using System.Threading.Tasks;

namespace RealEstateBackend.Services
{
    public interface IRentCastService
    {
        Task<string> GetPropertyDetailsAsync(string address);
    }
}
