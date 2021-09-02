using TaxJarApi.Models;
using TaxJarApi.Services;

namespace TaxJarApi
{
    public class TaxJarClient
    {
        IIntegrationService service;
        public TaxJarClient(string address, string token)
        {
            service = new TaxJarService(address, token);
        }

        public RateForLocationResponse GetRate(string zip, object parameters = null)
        {
            var newRate = service.GetRate<RateResponse>(zip, parameters).Result;
            if (newRate != null)
            {
                return newRate.Rate;
            }
            else
            {
                return default(RateForLocationResponse);
            }
        }

        public TaxForOrderResponse GetOrderTax(object orderData)
        {
            var newTax = service.GetTax<TaxResponse>(orderData).Result;
            if (newTax != null)
            {
                return newTax.Tax;
            }
            else
            {
                return default(TaxForOrderResponse);
            }
        }

    }
}
