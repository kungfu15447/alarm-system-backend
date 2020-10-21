using System.Threading.Tasks;
using AlarmSystem.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;
namespace Test.Functions
{
    public class HttpExampleTest 
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async Task TestFunctionReturnsOkObjectResult() 
        {
            //Given
            var req = new HttpRequestBuilder().Query("name", "billy").Build();

            //When
            var res = await HttpExample.Run(req, logger);

            //Then
            Assert.IsType<OkObjectResult>(res);
        }

        [Theory]
        [InlineData("Billy")]
        [InlineData("Jimmy")]
        [InlineData("Børge")]
        [InlineData("Lars Larsen Larsensen")]
        [InlineData("fjd8sdfyhdf98ghdf98gsdhf98gsd")]
        [InlineData("N")]
        public async Task FunctionShouldReturnSentenceWithNameFromBodyIfNameNotEmpty(string name)
        {
            //Given
            var body = new { name = name };

            var req = new HttpRequestBuilder().Body(body).Build();

            //When
            var res = (OkObjectResult)await HttpExample.Run(req, logger);

            //Then
            Assert.Equal($"Hello, {name}. This HTTP triggered function executed successfully.", res.Value);
        }

        [Theory]
        [InlineData("Billy")]
        [InlineData("Jimmy")]
        [InlineData("Børge")]
        [InlineData("Lars Larsen Larsensen")]
        [InlineData("fjd8sdfyhdf98ghdf98gsdhf98gsd")]
        [InlineData("N")]
        public async Task FunctionShouldReturnSentenceWithNameFromQueryIfNameNotEmpty(string name) 
        {
            //Given
            var req = new HttpRequestBuilder().Query("name", name).Build();

            //When
            var res = (OkObjectResult)await HttpExample.Run(req, logger);

            //Then
            Assert.Equal($"Hello, {name}. This HTTP triggered function executed successfully.", res.Value);
        }

        [Fact]
        public async Task FunctionShouldReturnSentenceWithoutNameIfNameIsEmpty()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            //When
            var res = (OkObjectResult)await HttpExample.Run(req, logger);

            //Then
            Assert.Equal("This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.", res.Value);
        }
    }
}