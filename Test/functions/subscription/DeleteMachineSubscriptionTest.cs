using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.DB;
using AlarmSystem.Functions.Subscription.DeleteMachineSubscription;
using AlarmSystem.Test.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.Subscription
{
    public class DeleteMachineSubscriptionTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();
        
        [Fact]
        public async Task TestFunctionShouldRerturnOkResultAsync()
        {
            //Given
            var body = new 
            {
                MachineId = "machine-test-1",
                WatchId = "watch-test-1"
            };

            var req = new HttpRequestBuilder().Body(body).Build();
            var watchService = new Mock<IWatchService>();

            //When
            watchService.Setup(ws => ws.DeleteMachineSubscriptionFromWatch(It.IsAny<MachineWatch>()));
            watchService.Setup(ws => ws.GetMachineSubcriptionOfMachineFromWatch(It.IsAny<string>(), It.IsAny<string>())).Returns(It.IsAny<MachineWatch>());

            var res = (OkResult) await new DeleteMachineSubscription(watchService.Object).Run(req, logger);
        
            //Then
            Assert.IsType<OkResult>(res);
        }

        [Fact]
        public async Task TestFunctionShouldCallServiceMethodsOnceAsync()
        {
            //Given
            var body = new 
            {
                MachineId = "machine-test-1",
                WatchId = "watch-test-1"
            };

            var req = new HttpRequestBuilder().Body(body).Build();
            var watchService = new Mock<IWatchService>();

            //When
            watchService.Setup(ws => ws.DeleteMachineSubscriptionFromWatch(It.IsAny<MachineWatch>()));
            watchService.Setup(ws => ws.GetMachineSubcriptionOfMachineFromWatch(It.IsAny<string>(), It.IsAny<string>())).Returns(It.IsAny<MachineWatch>());

            await new DeleteMachineSubscription(watchService.Object).Run(req, logger);
        
            //Then
            watchService.Verify(ws => ws.DeleteMachineSubscriptionFromWatch(It.IsAny<MachineWatch>()), Times.Once);
            watchService.Verify(ws => ws.GetMachineSubcriptionOfMachineFromWatch(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task TestFunctionShouldReturnBadRequestIfErrorIsThrownAsync()
        {
            //Given
            var body = new 
            {
                MachineId = "machine-test-1",
                WatchId = "watch-test-1"
            };

            var req = new HttpRequestBuilder().Body(body).Build();
            var watchService = new Mock<IWatchService>();

            //When
            watchService.Setup(ws => ws.DeleteMachineSubscriptionFromWatch(It.IsAny<MachineWatch>()));
            watchService.Setup(ws => ws.GetMachineSubcriptionOfMachineFromWatch(It.IsAny<string>(), It.IsAny<string>())).Throws<InvalidDataException>();

            var res = (BadRequestObjectResult) await new DeleteMachineSubscription(watchService.Object).Run(req, logger);
        
            //Then
            Assert.IsType<BadRequestObjectResult>(res);
            watchService.Verify(ws => ws.DeleteMachineSubscriptionFromWatch(It.IsAny<MachineWatch>()), Times.Never);
        }
    }
}