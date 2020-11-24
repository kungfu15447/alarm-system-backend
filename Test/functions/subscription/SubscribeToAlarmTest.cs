using AlarmSystem.Core.Application;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Entity.DB;
using AlarmSystem.Functions.Subscription.SubscribeToAlarmFunction;
using AlarmSystem.Test.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.Subscription
{
    public class SubscribeToAlarmTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void FunctionShouldCallWatchServiceOnce() {
            //Given
            var body = new SubscribeToAlarmModel() {
                AlarmId = 1,
                WatchId = "1"
            };

            var req = new HttpRequestBuilder().Body(body).Build();
            var watchService = new Mock<IWatchService>();
            var alarmService = new Mock<IAlarmService>();
            
            AlarmWatch aw = new AlarmWatch{
                Alarm = new AlarmSystem.Core.Entity.DB.Alarm {
                    AlarmId = 1,
                    Code = 205,
                    Description = "TestDescription"
                },
                WatchId = "1"
            };

            //When
            watchService.Setup(ms => ms.SubscribeToAlarm(aw));

            var res = await new SubscribeToAlarm(watchService.Object, alarmService.Object).Run(req, logger);

            //Then
            watchService.Verify(ms => ms.SubscribeToAlarm(It.IsAny<AlarmWatch>()), Times.Once());
            
        }

        [Fact]
        public async void FunctionShouldReturnOkResult()
        {
            //Given
            var body = new SubscribeToAlarmModel() {
                AlarmId = 1,
                WatchId = "1"
            };
            var req = new HttpRequestBuilder().Body(body).Build();
            var watchService = new Mock<IWatchService>();
            var alarmService = new Mock<IAlarmService>();

            //When
            var res = (OkResult)await new SubscribeToAlarm(watchService.Object, alarmService.Object).Run(req, logger);

            //Then
            Assert.IsType<OkResult>(res);
        }
    }
}