using NUnit.Framework;
using RestSharp;

namespace DailyExcuses.Tests
{
    [TestFixture]
    public class RandomExcuseControllerTests
    {
        private RestClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient("https://dailyexcuses.azurewebsites.net");
        }

        [Test]
        public void Test_GetQuote_DefaultLanguage()
        {
            var request = new RestRequest("/quote");
            var response = _client.Get(request);

            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Test]
        public void Test_GetQuote_WithLanguage()
        {
            var request = new RestRequest("/quote?lang=pt");
            var response = _client.Get(request);

            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Test]
        public void Test_GetWymowka_ForPolish()
        {
            var request = new RestRequest("/wymowka");
            var response = _client.Get(request);

            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Test]
        public void Test_GetWymowka_ForPortuguese()
        {
            var request = new RestRequest("/escusa");
            var response = _client.Get(request);

            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Test]
        public void Test_GetWymowka_ForEnglish()
        {
            var request = new RestRequest("/excuse");
            var response = _client.Get(request);

            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Test]
        [Ignore("Difficult to track file versions!")]
        public void Test_InsertNew()
        {
            var request = new RestRequest("/new", Method.Post);
            request.AddParameter("application/json", "Test excuse", ParameterType.RequestBody);
            var response = _client.Execute(request);

            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Test]
        public void Test_GetList_DefaultLanguage()
        {
            var request = new RestRequest("/list");
            var response = _client.Get(request);

            Assert.AreEqual(200, (int)response.StatusCode);
        }

        [Test]
        public void Test_GetList_WithLanguage()
        {
            var request = new RestRequest("/list?lang=pl");
            var response = _client.Get(request);

            Assert.AreEqual(200, (int)response.StatusCode);
        }
    }
}