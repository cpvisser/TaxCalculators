using System.Threading.Tasks;

namespace TaxJarApi.Services
{    public interface IIntegrationService
    {
        Task<T> GetRate<T>(string zip, object rateData);

        Task<T> GetTax<T>(object orderData);
    }
}
