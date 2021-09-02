using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.IO;
using TaxJarApi;


namespace TaxJarClientTest
{
    public class RateTests
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
        public void FailureTest()
        {
            var rateData = client.GetRate("");

            // verify that the response is null
            Assert.IsNull(rateData);
        }

        [Test]
        public void GetRateForValidUSZip5()
        {
            string testZip = "27253";
            var rateData = client.GetRate(testZip);

            // verify response includes data
            Assert.IsNotNull(rateData);

            // compare certain data points
            Assert.AreEqual(testZip, rateData.Zip);
            Assert.IsNotEmpty(rateData.State, "Returned state is empty.");
            Assert.IsNotEmpty(rateData.City, "Returned city is empty.");
            Assert.IsNotNull(rateData.CombinedRate, "No rate was returned.");
        }

        [Test]
        public void GetRateForValidUSZip9()
        {
            string testZip = "90404-3370";
            var rateData = client.GetRate(testZip);

            // verify response includes data
            Assert.IsNotNull(rateData);

            // compare certain data points
            Assert.AreEqual(testZip, rateData.Zip);
            Assert.IsNotEmpty(rateData.State, "Returned state is empty.");
            Assert.IsNotEmpty(rateData.City, "Returned city is empty.");
            Assert.IsNotNull(rateData.CombinedRate, "No rate was returned.");
        }

        [Test]
        public void GetRateForValidCAZip()
        {
            string testZip = "M4C1C9";
            object parameters = new
            {
                Country = "CA",
                Zip = testZip
            };
            var rateData = client.GetRate(testZip, parameters);

            // verify response includes data
            Assert.IsNotNull(rateData);

            // compare certain data points
            Assert.AreEqual(testZip, rateData.Zip);
            Assert.IsNotEmpty(rateData.State, "Returned state is empty.");
            Assert.IsNotEmpty(rateData.City, "Returned city is empty.");
            Assert.IsNotNull(rateData.CombinedRate, "No rate was returned.");
        }
    }
}