using SearchMvc.Services;
using Xunit;

namespace SearchMvc.Tests.Services
{
    public class MatchServiceTest
    {
        [Fact]
        public void should_return_zero_when_input_is_empty()
        {
            // Arrange
            var input = string.Empty;
            var url = "www.sympli.com.au";

            var sut = new MatchService();

            // Act
            var result = sut.Count(input, url);

            // Assert
            Assert.Equal(result, 0);
        }

        [Theory]
        [InlineData("<cite>https://www.sympli.com.au</cite>", "www.sympli.com.au")]
        [InlineData("<cite>https://www.sympli.com.au/foo</cite>", "www.sympli.com.au")]
        public void should_return_correct_count(string input, string url)
        {
            // Arrange
            var sut = new MatchService();

            // Act
            var result = sut.Count(input, url);

            // Assert
            Assert.Equal(result, 1);
        }
    }
}
