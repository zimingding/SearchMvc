using System;
using System.Net.Http;
using Moq;
using SearchMvc.Models;
using SearchMvc.Services;
using Xunit;

namespace SearchMvc.Tests
{
    public class GoogleSearchServiceTest
    {
        [Fact]
        public void Test1()
        {
            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            var sut = new GoogleSearchService(mockHttpClientWrapper.Object);
            var request = new SearchRequest
            {
                Keywords = "online title search, e-settlement",
                Url = "www.sympli.com.au"
            };

            // Act
            sut.Search(request);

            mockHttpClientWrapper.Verify(x => x.SendAsync(It.IsAny<HttpRequestMessage>()), Times.Once);
        }
    }
}
