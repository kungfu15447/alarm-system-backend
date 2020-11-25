using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Core.Entity.DB;
using AlarmSystem.Functions.Subscription;
using AlarmSystem.Test.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.Subscription
{
    
    public class GetMachineSubscriptionsTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Theory]
        [InlineData("test-id-1")]
        [InlineData("test-id-2")]
        [InlineData("test-id-3")]
        [InlineData("4")]
        public async Task TestFunctionReturnsResultOfTypeOkObjectResultAsync(string watchId)
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IWatchService>();
            List<MachineWatch> subscriptions = new List<MachineWatch>()
            {
                new MachineWatch 
                { 
                    Machine = new AlarmSystem.Core.Entity.DB.Machine 
                    { 
                        MachineId = "machine-id-1"
                    },
                    WatchId = "watch-id-1"
                }
            };

            //When
            mockService.Setup(ms => ms.GetMachineSubscriptionsFromWatch(It.IsAny<string>())).Returns(subscriptions);

            var res = (OkObjectResult) await new GetMachineSubscriptions(mockService.Object).Run(req, logger, watchId);

            //Then
            Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async Task TestFunctionReturnsNoContentResultIfListIsEmptyAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IWatchService>();
            string watchId = "watch-id-1";
            List<MachineWatch> subscriptions = new List<MachineWatch>();

            //When
            mockService.Setup(ms => ms.GetMachineSubscriptionsFromWatch(It.IsAny<string>())).Returns(subscriptions);

            var res = (NoContentResult) await new GetMachineSubscriptions(mockService.Object).Run(req, logger, watchId);

            //Then
            Assert.IsType<NoContentResult>(res);
        }

        [Fact]
        public async Task TestFunctionReturnsBadRequestObjectResultIfInvalidDataExceptionIsThrownAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IWatchService>();
            string watchId = "watch-id-1";

            //When
            mockService.Setup(ms => ms.GetMachineSubscriptionsFromWatch(It.IsAny<string>())).Throws<InvalidDataException>();

            var res = (BadRequestObjectResult) await new GetMachineSubscriptions(mockService.Object).Run(req, logger, watchId);

            //Then
            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public async Task TestFunctionShouldReturnErrorMessageIfExceptionIsThrownAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IWatchService>();
            string watchId = "watch-id-1";

            //When
            mockService.Setup(ms => ms.GetMachineSubscriptionsFromWatch(It.IsAny<string>())).Throws<InvalidDataException>();

            var res = (BadRequestObjectResult) await new GetMachineSubscriptions(mockService.Object).Run(req, logger, watchId);
            
            //Then
            Assert.False(String.IsNullOrEmpty((string) res.Value));
        }

        [Fact]
        public async Task TestFunctionServiceShouldOnlyBeCalledOnceAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IWatchService>();
            string watchId = "watch-id-1";
            List<MachineWatch> subscriptions = new List<MachineWatch>();

            //When
            mockService.Setup(ms => ms.GetMachineSubscriptionsFromWatch(It.IsAny<string>())).Returns(subscriptions);

            await new GetMachineSubscriptions(mockService.Object).Run(req, logger, watchId);

            //Then
            mockService.Verify(ms => ms.GetMachineSubscriptionsFromWatch(It.IsAny<string>()), Times.Once);
        }

    }
}