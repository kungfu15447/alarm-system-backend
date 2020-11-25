using System.Collections.Generic;
using AlarmSystem.Test.Utils;
using AlarmSystem.Core.Entity.Dto;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using AlarmSystem.Core.Application;
using AlarmSystem.Functions.Alarm;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;

namespace AlarmSystem.Test.Functions.Alarm
{
    public class GetAlarmsWithSubsTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public void FunctionShouldCallGetAllMachinesWithSubsFromServiceOnce()
        {
            //Given
            var machineService = new Mock<IAlarmService>();
            var req = new HttpRequestBuilder().Build();

            //When
            machineService.Setup(ms => ms.GetAllAlarmsWithSubs(It.IsAny<string>())).Returns(It.IsAny<List<AlarmWithSubscription>>());

            var res = new GetAlarmsWithSubs(machineService.Object).Run(req, logger, It.IsAny<string>());

            //Then
            machineService.Verify(ms => ms.GetAllAlarmsWithSubs(It.IsAny<string>()), Times.Once);
        }
        
        [Theory]
        [InlineData("test-id-1")]
        [InlineData("test-id-2")]
        [InlineData("test-id-3")]
        [InlineData("4")]
        public async Task TestFunctionReturnsResultOfTypeOkObjectResultAsync(string watchId)
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IAlarmService>();
            List<AlarmWithSubscription> subscriptions = new List<AlarmWithSubscription>()
            {
                new AlarmWithSubscription
                { 
                    AlarmId = 2,
                    Code = 1,
                    Description = "desc",
                    IsSubscribed = false
                }
            };

            //When
            mockService.Setup(ms => ms.GetAllAlarmsWithSubs(It.IsAny<string>())).Returns(subscriptions);

            var res = (OkObjectResult) await new GetAlarmsWithSubs(mockService.Object).Run(req, logger, watchId);

            //Then
            Assert.IsType<OkObjectResult>(res);
        }

         [Fact]
        public async Task TestFunctionShouldReturnNoContentResultIfListIsEmptyAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IAlarmService>();
            var watchId = "watch-id-1";
            List<AlarmWithSubscription> alarmsWithSubs = new List<AlarmWithSubscription>();
        
            //When
            mockService.Setup(ms => ms.GetAllAlarmsWithSubs(It.IsAny<string>())).Returns(alarmsWithSubs);

            var res = (NoContentResult) await new GetAlarmsWithSubs(mockService.Object).Run(req, logger, watchId);
            
            //Then
            Assert.IsType<NoContentResult>(res);
        }

        [Fact]
        public async Task TestFunctionShouldReturnBadRequestObjectResultIfInvalidDataExceptionIsThrownAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IAlarmService>();
            var watchId = "watch-id-1";

            //When
            mockService.Setup(ms => ms.GetAllAlarmsWithSubs(It.IsAny<string>())).Throws<InvalidDataException>();

            var res = (BadRequestObjectResult) await new GetAlarmsWithSubs(mockService.Object).Run(req, logger, watchId);
            
            //Then
            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public async Task TestFunctionShouldIncludeErrorMessageInBadRequestObjectResultAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IAlarmService>();
            var watchId = "watch-id-1";

            //When
            mockService.Setup(ms => ms.GetAllAlarmsWithSubs(It.IsAny<string>())).Throws<InvalidDataException>();

            var res = (BadRequestObjectResult) await new GetAlarmsWithSubs(mockService.Object).Run(req, logger, watchId);
            
            //Then
            Assert.False(String.IsNullOrEmpty((string) res.Value));
        }
    }
}