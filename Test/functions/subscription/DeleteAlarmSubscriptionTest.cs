using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Functions.Subscription.DeleteAlarmSubscriptionFunction;
using AlarmSystem.Test.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.Subscription
{
    public class DeleteAlarmSubscriptionTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();
        
        [Fact]
        public async Task TestFunctionShouldReturnOkResultIfSubscripstionIsRemovedAsync()
        {
            //Given
            var body = new 
            {
                WatchId = "watch-id-1",
                AlarmId = 1
            };
            var req = new HttpRequestBuilder().Body(body).Build();
            var watchService = new Mock<IWatchService>();
        
            //When
            watchService.Setup(ws => ws.DeleteAlarmSubscriptionFromWatch(It.IsAny<AlarmWatch>()));
            watchService.Setup(ws => ws.GetSubscriptionOfAlarmFromWatch(It.IsAny<int>(), It.IsAny<string>())).Returns(It.IsAny<AlarmWatch>());

            var res = (OkResult) await new DeleteAlarmSubscription(watchService.Object).Run(req, logger);

            //Then
            Assert.IsType<OkResult>(res);
            watchService.Verify(ws => ws.DeleteAlarmSubscriptionFromWatch(It.IsAny<AlarmWatch>()), Times.Once);
            watchService.Verify(ws => ws.GetSubscriptionOfAlarmFromWatch(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task TestFunctionShouldReturnBadRequestIfExceptionIsThrownAsync()
        {
            //Given
            var body = new 
            {
                WatchId = "watch-id-1",
                AlarmId = 1
            };
            var req = new HttpRequestBuilder().Body(body).Build();
            var watchService = new Mock<IWatchService>();
        
            //When
            watchService.Setup(ws => ws.DeleteAlarmSubscriptionFromWatch(It.IsAny<AlarmWatch>()));
            watchService.Setup(ws => ws.GetSubscriptionOfAlarmFromWatch(It.IsAny<int>(), It.IsAny<string>())).Throws<InvalidDataException>();

            var res = (BadRequestObjectResult) await new DeleteAlarmSubscription(watchService.Object).Run(req, logger);

            //Then
            Assert.IsType<BadRequestObjectResult>(res);
            watchService.Verify(ws => ws.DeleteAlarmSubscriptionFromWatch(It.IsAny<AlarmWatch>()), Times.Never);
            watchService.Verify(ws => ws.GetSubscriptionOfAlarmFromWatch(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }
    }
}