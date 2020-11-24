using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.DB;
using AlarmSystem.Functions.Subscription;
using AlarmSystem.Test.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.Subscription
{
    public class GetAlarmSubscriptionsTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async Task TestFunctionShouldCallServiceMethodOnceAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IWatchService>();
            var watchId = "watch-id-1";
            List<AlarmWatch> subscriptions = new List<AlarmWatch>();
        
            //When
            mockService.Setup(ms => ms.GetAlarmSubscriptionsFromWatch(It.IsAny<string>())).Returns(subscriptions);

            await new GetAlarmSubscriptions(mockService.Object).Run(req, logger, watchId);
            
            //Then
            mockService.Verify(ms => ms.GetAlarmSubscriptionsFromWatch(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task TestFunctionShouldReturnNoContentResultIfListIsEmptyAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IWatchService>();
            var watchId = "watch-id-1";
            List<AlarmWatch> subscriptions = new List<AlarmWatch>();
        
            //When
            mockService.Setup(ms => ms.GetAlarmSubscriptionsFromWatch(It.IsAny<string>())).Returns(subscriptions);

            var res = (NoContentResult) await new GetAlarmSubscriptions(mockService.Object).Run(req, logger, watchId);
            
            //Then
            Assert.IsType<NoContentResult>(res);
        }

        [Fact]
        public async Task TestFunctionShouldReturnOkObjectResultIfListIsNotEmptyAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IWatchService>();
            var watchId = "watch-id-1";
            List<AlarmWatch> subscriptions = new List<AlarmWatch>(){
                new AlarmWatch {
                    Alarm = new Core.Entity.DB.Alarm {
                        AlarmId = 1
                    },
                    WatchId = watchId
                }
            };
        
            //When
            mockService.Setup(ms => ms.GetAlarmSubscriptionsFromWatch(It.IsAny<string>())).Returns(subscriptions);

            var res = (OkObjectResult) await new GetAlarmSubscriptions(mockService.Object).Run(req, logger, watchId);
            
            //Then
            Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async Task TestFunctionShouldReturnBadRequestObjectResultIfInvalidDataExceptionIsThrownAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IWatchService>();
            var watchId = "watch-id-1";

            //When
            mockService.Setup(ms => ms.GetAlarmSubscriptionsFromWatch(It.IsAny<string>())).Throws<InvalidDataException>();

            var res = (BadRequestObjectResult) await new GetAlarmSubscriptions(mockService.Object).Run(req, logger, watchId);
            
            //Then
            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public async Task TestFunctionShouldIncludeErrorMessageInBadRequestObjectResultAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IWatchService>();
            var watchId = "watch-id-1";

            //When
            mockService.Setup(ms => ms.GetAlarmSubscriptionsFromWatch(It.IsAny<string>())).Throws<InvalidDataException>();

            var res = (BadRequestObjectResult) await new GetAlarmSubscriptions(mockService.Object).Run(req, logger, watchId);
            
            //Then
            Assert.False(String.IsNullOrEmpty((string) res.Value));
        }
    }
}