using NUnit.Framework;
using System;

namespace Binjy.HackerNews.Core.Service.Tests
{
    [TestFixture()]
    public class RestApiConfigTests
    {
        [Test()]
        public void Should_Initialize_ValidDefaults_Test()
        {
            var endpoint = new RestApiConfig();

            Assert.IsTrue(endpoint.BaseUri.Equals("hacker-news.firebaseio.com"));
            Assert.IsTrue(endpoint.ApiVersion.Equals("v0"));
            Assert.IsTrue(endpoint.IsSecure.Equals(true));
        }

        [Test()]
        public void AsIndexEndpointUrl_Should_Return_Valid_IndexUri(
            [Values("topstories", "newstories", "beststories")] string endpointName)
        {
            var endpoint = new RestApiConfig();
            var indexUrl = endpoint.AsIndexEndpointUrl(endpointName);

            Assert.AreEqual(indexUrl.AbsoluteUri, $"https://hacker-news.firebaseio.com/v0/{endpointName}.json");
        }

        [Test()]
        public void AsItemEndpointUrl_Should_Return_Valid_IndexUri(
            [Values("1", "22015", "32043")] int id
        )
        {
            var endpoint = new RestApiConfig();
            var itemUrl = endpoint.AsItemEndpointUrl(id);

            Assert.AreEqual(itemUrl.AbsoluteUri, $"https://hacker-news.firebaseio.com/v0/item/{id}.json");
        }

        [Test()]
        public void AsItemEndpointUrl_With_Id_Less_Than_One_Should_Throw_ArgumentOutOfRangeException(
            [Values("0", "-1", "-32043")] int negativeId)
        {

            var endpoint = new RestApiConfig();

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(
                delegate { endpoint.AsItemEndpointUrl(negativeId); });

            Assert.That(ex.ParamName, Is.EqualTo("id"));

        }

        [Test()]
        public void AsIndexEndpointUrl_With_Null_BaseUri_Should_Throw_ArgumentNullException()
        {
            var endpoint = new RestApiConfig { BaseUri = null };

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(
                delegate { endpoint.AsIndexEndpointUrl("test"); });

            Assert.That(ex.ParamName, Is.EqualTo(nameof(endpoint.BaseUri)));
        }

        [Test()]
        public void AsIndexEndpointUrl_With_Null_ApiVersion_Should_Throw_ArgumentNullException()
        {
            var endpoint = new RestApiConfig { ApiVersion = null };

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(
                delegate { endpoint.AsIndexEndpointUrl("topstories"); });

            Assert.That(ex.ParamName, Is.EqualTo(nameof(endpoint.ApiVersion)));
        }

    }
}