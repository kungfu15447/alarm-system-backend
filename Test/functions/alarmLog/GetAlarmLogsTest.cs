
using AlarmSystem.Core.Application;
using AlarmSystem.Functions.AlarmLog;
using AlarmSystem.Test.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.AlarmLog
{
    public class GetAlarmLogsTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void FunctionShouldCallAlarmLogServiceOnce() {
            //Given
            var token = "mocktoken";
            var req = new HttpRequestBuilder().AuthHeader(token).Build();
            var alarmLogService = new Mock<IAlarmLogService>();
            var authService = new Mock<IAuthenticationService>();

            //When
            authService.Setup(aus => aus.DecryptToken(It.IsAny<string>())).Returns(true);
            alarmLogService.Setup(als => als.GetAlarmLog());

            var res = await new GetAlarmLogs(alarmLogService.Object, authService.Object).Run(req, logger);

            //Then
            alarmLogService.Verify(ms => ms.GetAlarmLog(), Times.Once());
        }

        [Fact]
        public async void FunctionShouldReturnOkObjectResult()
        {
            //Given
            var token = "mocktoken";
            var req = new HttpRequestBuilder().AuthHeader(token).Build();
            var alarmLogService = new Mock<IAlarmLogService>();
            var authService = new Mock<IAuthenticationService>();

            //When
            authService.Setup(aus => aus.DecryptToken(It.IsAny<string>())).Returns(true);
            alarmLogService.Setup(als => als.GetAlarmLog());

            var res = (OkObjectResult) await new GetAlarmLogs(alarmLogService.Object, authService.Object).Run(req, logger);

            //Then
            Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async void FunctionShouldReturnUnauthorisedResultWhenTokenCannotBeDecrypted(){
            //Given
            var token = "mocktoken";
            var req = new HttpRequestBuilder().AuthHeader(token).Build();
            var alarmLogService = new Mock<IAlarmLogService>();
            var authService = new Mock<IAuthenticationService>();

            //When
            authService.Setup(aus => aus.DecryptToken(It.IsAny<string>())).Returns(false);
            alarmLogService.Setup(als => als.GetAlarmLog());

            var res = (UnauthorizedResult) await new GetAlarmLogs(alarmLogService.Object, authService.Object).Run(req, logger);

            //Then
            Assert.IsType<UnauthorizedResult>(res);
        }

        [Fact]
        public async void FunctionShouldReturnUnauthorisedResultWhenNoTokenIsGiven(){
            string token = null;
            var req = new HttpRequestBuilder().AuthHeader(token).Build();
            var alarmLogService = new Mock<IAlarmLogService>();
            var authService = new Mock<IAuthenticationService>();

            //When
            authService.Setup(aus => aus.DecryptToken(It.IsAny<string>())).Returns(false);
            alarmLogService.Setup(als => als.GetAlarmLog());

            var res = (UnauthorizedResult) await new GetAlarmLogs(alarmLogService.Object, authService.Object).Run(req, logger);

            //Then
            Assert.IsType<UnauthorizedResult>(res);
        }


    }
}