using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.IO;
using TaxJarApi;

namespace TaxJarClientTest
{
    public class TaxTests
    {
        internal TaxJarClient client;
        [SetUp]
        public void Init()
        {
            string address = "", token = "";
            var secrets = File.ReadAllText("../../../secrets.json");
            var options = JObject.Parse(secrets);
            address = (string)options.SelectToken("ApiAccess[0].TaxJar.BaseAddress");
            token = (string)options.SelectToken("ApiAccess[0].TaxJar.Token");

            client = new TaxJarClient(address, token);
        }

        [Test]
        public void GetTaxForValidOrder()
        {
            // example from API specification
            object orderData = new
            {
                from_country = "US",
                from_zip = "92093",
                from_state = "CA",
                from_city = "La Jolla",
                from_street = "9500 Gilman Drive",
                to_country = "US",
                to_zip = "90002",
                to_state = "CA",
                to_city = "Los Angeles",
                to_street = "1335 E 103rd St",
                amount = 15,
                shipping = 1.5,
                nexus_addresses = new[]
                {
                    new 
                    {
                        id = "Main Location",
                        country = "US",
                        zip="92093",
                        state = "CA",
                        city = "La Jolla",
                        street = "9500 Gilman Drive"
                    }
                },
                line_items = new[]
                {
                    new
                    {
                        id = "1",
                        quantity = 1,
                        product_tax_code  = "20010",
                        unit_price  = 15,
                        discount = 0
                    }
                }
            };

            var taxData = client.GetOrderTax(orderData);

            // verify response includes data
            Assert.IsNotNull(taxData);

            // compare certain data points
            Assert.AreEqual(16.5, taxData.OrderTotalAmount);
            Assert.AreEqual(1.5, taxData.Shipping);
            Assert.AreEqual(15, taxData.TaxableAmount);
            Assert.AreEqual("1", taxData.Breakdown.LineItems[0].Id);
            Assert.AreEqual(0.0625, taxData.Breakdown.LineItems[0].StateSalesTaxRate);
        }
    }
}
