using NUnit.Framework;
using RestSharp;

namespace DailyExcuses.Tests
{
    [TestFixture]
    public class RandomExcuseControllerTests
    {
        private RestClient _client;
        private const int OK = 200;

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

            Assert.That((int)response.StatusCode, Is.EqualTo(OK));
        }

        [Test]
        public void Test_GetQuote_WithLanguage()
        {
            var request = new RestRequest("/quote?lang=pt");
            var response = _client.Get(request);

            Assert.That((int)response.StatusCode, Is.EqualTo(OK));
        }

        [Test]
        public void Test_GetWymowka_ForPolish()
        {
            var request = new RestRequest("/wymowka");
            var response = _client.Get(request);

            Assert.That((int)response.StatusCode, Is.EqualTo(OK));
        }

        [Test]
        public void Test_GetWymowka_ForPortuguese()
        {
            var request = new RestRequest("/escusa");
            var response = _client.Get(request);

            Assert.That((int)response.StatusCode, Is.EqualTo(OK));
        }

        [Test]
        public void Test_GetWymowka_ForEnglish()
        {
            var request = new RestRequest("/excuse");
            var response = _client.Get(request);

            Assert.That((int)response.StatusCode, Is.EqualTo(OK));
        }

        [Test]
        [Ignore("Difficult to track file versions!")]
        public void Test_InsertNew()
        {
            var request = new RestRequest("/new", Method.Post);
            request.AddParameter("application/json", "Test excuse", ParameterType.RequestBody);
            var response = _client.Execute(request);

            Assert.That((int)response.StatusCode, Is.EqualTo(OK));
        }

        [Test]
        public void Test_GetList_DefaultLanguage()
        {
            var request = new RestRequest("/list");
            var response = _client.Get(request);

            Assert.That((int)response.StatusCode, Is.EqualTo(OK));
        }

        [Test]
        public void Test_GetList_WithLanguage()
        {
            var request = new RestRequest("/list?lang=pl");
            var response = _client.Get(request);

            Assert.That((int)response.StatusCode, Is.EqualTo(OK));
        }
    }
}