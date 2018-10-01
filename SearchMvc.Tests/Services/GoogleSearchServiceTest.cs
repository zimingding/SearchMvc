using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using SearchMvc.Models;
using SearchMvc.Services;
using Xunit;

namespace SearchMvc.Tests
{
    public class GoogleSearchServiceTest
    {
        [Fact]
        public async Task should_call_httpclient_and_matchservice_and_return_result()
        {
            // Arrange
            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>())).ReturnsAsync("search result");
            var mockMatchService = new Mock<IMatchService>();
            mockMatchService.Setup(x => x.Count(It.IsAny<string>(), It.IsAny<string>())).Returns(1);
            var mockSearchOptions = new Mock<IOptions<GoogleSearchOptions>>();
            mockSearchOptions.Setup(x => x.Value).Returns(new GoogleSearchOptions());

            var sut = new GoogleSearchService(mockHttpClientWrapper.Object, mockMatchService.Object, mockSearchOptions.Object);
            var request = new SearchRequest
            {
                Keywords = "online title search",
                Url = "www.sympli.com.au"
            };

            // Act
            var result = await sut.Search(request);

            // Assert
            mockHttpClientWrapper.Verify(x => x.SendAsync(It.IsAny<HttpRequestMessage>()), Times.Once);
            mockMatchService.Verify(x => x.Count("search result", request.Url), Times.Once);
            Assert.Equal(result.Length, 1);
            Assert.Equal(result[0], 1);
        }
    }
}
