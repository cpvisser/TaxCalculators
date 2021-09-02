using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaxJarApi.Models;
using RestSharp;

namespace TaxJarApi.Services
{
    public class TaxJarService : IIntegrationService
    {
        private static RestClient client;
        public TaxJarService(string address, string token)
        {
            client = new RestClient(new Uri(address));
            client.AddDefaultHeader("Authorization", $"Bearer {token}");
        }

        public async Task<RateResponse> GetRate<RateResponse>(string zip, object rateData = null)
        {
            try
            {
                var request = new RestRequest("rates?zip=" + zip, Method.GET, DataFormat.Json);

                if (rateData != null)
                {
                    foreach (var prop in rateData.GetType().GetProperties())
                    {
                        if (prop.GetValue(rateData) != null)
                        {
                            request.AddParameter(prop.Name.ToLower(), prop.GetValue(rateData).ToString());
                        }
                    }
                }

                var response = await client.ExecuteAsync<RateForLocationResponse>(request).ConfigureAwait(false);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<RateResponse>(response.Content);
                }
                else
                {
                    return default(RateResponse);
                }
            }
            catch (Exception ex)
            {
                // log exception
                Console.WriteLine("An error occurred in GetRate(); " + ex.Message + "; Inner Exception: " + ex.InnerException);
                return default(RateResponse);
            }
        }

        public async Task<TaxResponse> GetTax<TaxResponse>(object orderData)
        {
            try
            {
                var request = new RestRequest("taxes", Method.POST, DataFormat.Json);
                request.AddParameter("application/json", JsonConvert.SerializeObject(orderData), ParameterType.RequestBody);
                
                var response = await client.ExecuteAsync<TaxForOrderResponse>(request).ConfigureAwait(false);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<TaxResponse>(response.Content);
                }
                else
                {
                    return default(TaxResponse);
                }
            }
            catch (Exception ex)
            {
                // log exception
                Console.WriteLine("An error occurred in GetTax(); " + ex.Message + "; Inner Exception: " + ex.InnerException);
                return default(TaxResponse);
            }
        }
    }
}
