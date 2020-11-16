using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Functions.Notification;
using AlarmSystem.Functions.Notification.NotificationSettings;
using AlarmSystem.Test.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.Notifitcation
{
    public class SendAlertTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();
        
        [Fact]
        public async Task TestFunctionShouldReturnOkResultAsync()
        {
            //Given
            var alarmService = new Mock<IAlarmService>();
            var watchService = new Mock<IWatchService>();
            var notificationConnectionSetting = new Mock<INotificationHubConnectionSettings>();
            var notificationHub = new Mock<INotificationHubClient>();

            var body = new { MachinasdfeId = "test-id-1", AlarmCode = 42 };

            var req = new HttpRequestBuilder().Body(body).Build();

            var alarmSubs = new List<AlarmWatch>() { new AlarmWatch { Alarm = new Alarm { Code = 42, Description = "alarm-desc" }, WatchId = "watch-test-id-1" }};
            var machineSubs = new List<MachineWatch>() { new MachineWatch { Machine = new Core.Entity.Dto.Machine { MachineId = "test-id-1" }, WatchId = "watch-test-id-1" } };

            //When
            alarmService.Setup(als => als.GetAlarmByCode(It.IsAny<int>())).Returns(It.IsAny<Alarm>());
            watchService.Setup(ws => ws.GetAlarmSubscriptionsByAlarmCode(It.IsAny<int>())).Returns(alarmSubs);
            watchService.Setup(ws => ws.GetMachineSubscriptionsByMachine(It.IsAny<string>())).Returns(machineSubs);
            notificationHub.Setup(nh => nh.SendDirectNotificationAsync(It.IsAny<Notification>(), It.IsAny<string>()));
            notificationConnectionSetting.Setup(ncs => ncs.Hub).Returns(notificationHub.Object);

            var res = (OkResult) await new SendAlert(watchService.Object, alarmService.Object, notificationConnectionSetting.Object).Run(req, logger);

            //Then
            Assert.IsType<OkResult>(res);
        }

        [Fact]
        public async Task TestFunctionShouldReturnNoContentResultIfNoSubscriptionsWhereFoundAsync()
        {
            //Given
            var alarmService = new Mock<IAlarmService>();
            var watchService = new Mock<IWatchService>();
            var notificationConnectionSetting = new Mock<INotificationHubConnectionSettings>();
            var notificationHub = new Mock<INotificationHubClient>();

            var body = new { MachineId = "test-id-1", AlarmCode = 42 };

            var req = new HttpRequestBuilder().Body(body).Build();

            var alarmSubs = new List<AlarmWatch>();
            var machineSubs = new List<MachineWatch>();

            //When
            alarmService.Setup(als => als.GetAlarmByCode(It.IsAny<int>())).Returns(It.IsAny<Alarm>());
            watchService.Setup(ws => ws.GetAlarmSubscriptionsByAlarmCode(It.IsAny<int>())).Returns(alarmSubs);
            watchService.Setup(ws => ws.GetMachineSubscriptionsByMachine(It.IsAny<string>())).Returns(machineSubs);
            notificationHub.Setup(nh => nh.SendDirectNotificationAsync(It.IsAny<Notification>(), It.IsAny<string>()));
            notificationConnectionSetting.Setup(ncs => ncs.Hub).Returns(notificationHub.Object);

            var res = (NoContentResult) await new SendAlert(watchService.Object, alarmService.Object, notificationConnectionSetting.Object).Run(req, logger);

            //Then
            Assert.IsType<NoContentResult>(res);
        }

        [Fact]
        public async Task TestFunctionShouldGetAllSubscriptionsOnceAsync()
        {
            //Given
            var alarmService = new Mock<IAlarmService>();
            var watchService = new Mock<IWatchService>();
            var notificationConnectionSetting = new Mock<INotificationHubConnectionSettings>();
            var notificationHub = new Mock<INotificationHubClient>();

            var body = new { MachineId = "test-id-1", AlarmCode = 42 };

            var req = new HttpRequestBuilder().Body(body).Build();

            var alarmSubs = new List<AlarmWatch>();
            var machineSubs = new List<MachineWatch>();

            //When
            alarmService.Setup(als => als.GetAlarmByCode(It.IsAny<int>())).Returns(It.IsAny<Alarm>());
            watchService.Setup(ws => ws.GetAlarmSubscriptionsByAlarmCode(It.IsAny<int>())).Returns(alarmSubs);
            watchService.Setup(ws => ws.GetMachineSubscriptionsByMachine(It.IsAny<string>())).Returns(machineSubs);
            notificationHub.Setup(nh => nh.SendDirectNotificationAsync(It.IsAny<Notification>(), It.IsAny<string>()));
            notificationConnectionSetting.Setup(ncs => ncs.Hub).Returns(notificationHub.Object);

            var res = (NoContentResult) await new SendAlert(watchService.Object, alarmService.Object, notificationConnectionSetting.Object).Run(req, logger);

            //Then
            watchService.Verify(ws => ws.GetAlarmSubscriptionsByAlarmCode(It.IsAny<int>()), Times.Once);
            watchService.Verify(ws => ws.GetMachineSubscriptionsByMachine(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task TestFunctionShouldGetAlarmOnceAsync()
        {
            //Given
            var alarmService = new Mock<IAlarmService>();
            var watchService = new Mock<IWatchService>();
            var notificationConnectionSetting = new Mock<INotificationHubConnectionSettings>();
            var notificationHub = new Mock<INotificationHubClient>();

            var body = new { MachineId = "test-id-1", AlarmCode = 42 };

            var req = new HttpRequestBuilder().Body(body).Build();

            var alarmSubs = new List<AlarmWatch>();
            var machineSubs = new List<MachineWatch>();

            //When
            alarmService.Setup(als => als.GetAlarmByCode(It.IsAny<int>())).Returns(It.IsAny<Alarm>());
            watchService.Setup(ws => ws.GetAlarmSubscriptionsByAlarmCode(It.IsAny<int>())).Returns(alarmSubs);
            watchService.Setup(ws => ws.GetMachineSubscriptionsByMachine(It.IsAny<string>())).Returns(machineSubs);
            notificationHub.Setup(nh => nh.SendDirectNotificationAsync(It.IsAny<Notification>(), It.IsAny<string>()));
            notificationConnectionSetting.Setup(ncs => ncs.Hub).Returns(notificationHub.Object);

            var res = (NoContentResult) await new SendAlert(watchService.Object, alarmService.Object, notificationConnectionSetting.Object).Run(req, logger);

            //Then
            alarmService.Verify(als => als.GetAlarmByCode(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task TestFunctionShouldReturnBadRequestIfInvalidDataExceptionIsThrownWhenGettingSubscriptionsAsync()
        {
            //Given
            var alarmService = new Mock<IAlarmService>();
            var watchService = new Mock<IWatchService>();
            var notificationConnectionSetting = new Mock<INotificationHubConnectionSettings>();
            var notificationHub = new Mock<INotificationHubClient>();

            var body = new { MachineId = "test-id-1", AlarmCode = 42 };

            var req = new HttpRequestBuilder().Body(body).Build();

            var alarmSubs = new List<AlarmWatch>();

            //When
            alarmService.Setup(als => als.GetAlarmByCode(It.IsAny<int>())).Returns(It.IsAny<Alarm>());
            watchService.Setup(ws => ws.GetAlarmSubscriptionsByAlarmCode(It.IsAny<int>())).Returns(alarmSubs);
            watchService.Setup(ws => ws.GetMachineSubscriptionsByMachine(It.IsAny<string>())).Throws<InvalidDataException>();
            notificationHub.Setup(nh => nh.SendDirectNotificationAsync(It.IsAny<Notification>(), It.IsAny<string>()));
            notificationConnectionSetting.Setup(ncs => ncs.Hub).Returns(notificationHub.Object);

            var res = (BadRequestObjectResult) await new SendAlert(watchService.Object, alarmService.Object, notificationConnectionSetting.Object).Run(req, logger);

            //Then
            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public async Task TestFunctionShouldReturnErrorMessageIfExceptionIsThrownAsync()
        {
            //Given
            var alarmService = new Mock<IAlarmService>();
            var watchService = new Mock<IWatchService>();
            var notificationConnectionSetting = new Mock<INotificationHubConnectionSettings>();
            var notificationHub = new Mock<INotificationHubClient>();

            var body = new { MachineId = "test-id-1", AlarmCode = 42 };

            var req = new HttpRequestBuilder().Body(body).Build();

            var alarmSubs = new List<AlarmWatch>();

            //When
            alarmService.Setup(als => als.GetAlarmByCode(It.IsAny<int>())).Returns(It.IsAny<Alarm>());
            watchService.Setup(ws => ws.GetAlarmSubscriptionsByAlarmCode(It.IsAny<int>())).Returns(alarmSubs);
            watchService.Setup(ws => ws.GetMachineSubscriptionsByMachine(It.IsAny<string>())).Throws<InvalidDataException>();
            notificationHub.Setup(nh => nh.SendDirectNotificationAsync(It.IsAny<Notification>(), It.IsAny<string>()));
            notificationConnectionSetting.Setup(ncs => ncs.Hub).Returns(notificationHub.Object);

            var res = (BadRequestObjectResult) await new SendAlert(watchService.Object, alarmService.Object, notificationConnectionSetting.Object).Run(req, logger);

            //Then
            Assert.False(String.IsNullOrEmpty((string) res.Value));
        }
    }
}