using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Test.Functions {
    public class HttpExampleTest {

        [Fact]
        public async Task TestFunctionReturnsOkObjectResult() 
        {
            //Given
            var mockReq = new Mock<HttpRequest>(MockBehavior.Default);
            var mockLogger = new Mock<ILogger>();

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("");
            writer.Flush();
            stream.Position = 0;

            mockReq.Setup(x => x.Query["name"]).Returns("Billy");
            mockReq.Setup(req => req.Body).Returns(stream);

            //When
            var res = await HttpExample.Run(mockReq.Object, mockLogger.Object);

            //Then
            Assert.IsType<OkObjectResult>(res);
        }
    }
}