using Binjy.HackerNews.Core.Interface;
using Binjy.HackerNews.Core.Model;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Net.Http;

namespace Binjy.HackerNews.Core.Service.Tests
{
    public class HackerNewsRestClientTests
    {
        private IHackerNewsClient<Story> client;

        private IApiConfig config;

        //private HttpClient httpClient;

        [SetUp]
        public void Setup()
        {
            config = new Service.RestApiConfig
            {
                ApiVersion = "v0",
                BaseUri = "hacker-news.firebaseio.com",
                IsSecure = true
            };

            // Since HttpClient does not implement an interface, mocking by injecting
            // mocked HttpMessageHandler based on technique found at
            // https://gingter.org/2018/07/26/how-to-mock-httpclient-in-your-net-c-unit-tests/
            var handler = new Mock<HttpMessageHandler>();
            //var factory = handler.CreateClientFactory();

            var loggerMock = new Mock<ILogger<HackerNewsRestClient<Item>>>();
            var testLogger = loggerMock.Object;

            var mapperMock = new Mock<IItemMapper<Item>>();
            var testMapper = mapperMock.Object;

            //client = new HackerNewsRestClient<Item>(config, httpClient, testLogger, testMapper);
        }
    }
}