using AlarmSystem.Core.Application;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Test.Utils;
using functions.alarm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.Alarm
{
    public class GetAlarmByIdTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void FunctionShouldCallAlarmLogServiceOnce() {
            //Given
            var req = new HttpRequestBuilder().Build();
            var alarmService = new Mock<IAlarmService>();
            int mockAlarmID = 1;

            //When
            alarmService.Setup(ms => ms.GetAlarmById(mockAlarmID));

            var res = await new GetAlarmById(alarmService.Object).Run(req, logger, mockAlarmID);

            //Then
            alarmService.Verify(ms => ms.GetAlarmById(It.IsAny<int>()), Times.Once());
            
        }

        [Fact]
        public async void FunctionShouldReturnOkObjectResult()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var alarmService = new Mock<IAlarmService>();
            int mockAlarmID = 1;

            //When
            var res = (OkObjectResult)await new GetAlarmById(alarmService.Object).Run(req, logger, mockAlarmID);

            //Then
            Assert.IsType<OkObjectResult>(res);
        }
    }
}