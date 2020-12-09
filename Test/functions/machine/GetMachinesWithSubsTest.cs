using System.Collections.Generic;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.DB;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Functions.Machine;
using AlarmSystem.Functions.Subscription;
using AlarmSystem.Test.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.IO;
using System;

namespace AlarmSystem.Test.Functions.Machine
{
    public class GetMachinesWithSubsTest
    {

        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public void FunctionShouldCallGetAllMachinesWithSubsFromServiceOnce()
        {
            //Given
            var machineService = new Mock<IMachineService>();
            var req = new HttpRequestBuilder().Build();

            //When
            machineService.Setup(ms => ms.GetAllMachinesWithSubs(It.IsAny<string>())).Returns(It.IsAny<List<MachineWithSubscription>>());

            var res = new GetMachinesWithSubs(machineService.Object).Run(req, logger, It.IsAny<string>());

            //Then
            machineService.Verify(ms => ms.GetAllMachinesWithSubs(It.IsAny<string>()), Times.Once);
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
            var mockService = new Mock<IMachineService>();
            List<MachineWithSubscription> subscriptions = new List<MachineWithSubscription>()
            {
                new MachineWithSubscription
                { 
                    MachineId = "machine-id-1",
                    IsSubscribed = false
                }
            };

            //When
            mockService.Setup(ms => ms.GetAllMachinesWithSubs(It.IsAny<string>())).Returns(subscriptions);

            var res = (OkObjectResult) await new GetMachinesWithSubs(mockService.Object).Run(req, logger, watchId);

            //Then
            Assert.IsType<OkObjectResult>(res);
        }

         [Fact]
        public async Task TestFunctionShouldReturnNoContentResultIfListIsEmptyAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IMachineService>();
            var watchId = "watch-id-1";
            List<MachineWithSubscription> subscriptions = new List<MachineWithSubscription>();
        
            //When
            mockService.Setup(ms => ms.GetAllMachinesWithSubs(It.IsAny<string>())).Returns(subscriptions);

            var res = (NoContentResult) await new GetMachinesWithSubs(mockService.Object).Run(req, logger, watchId);
            
            //Then
            Assert.IsType<NoContentResult>(res);
        }

        [Fact]
        public async Task TestFunctionShouldReturnBadRequestObjectResultIfInvalidDataExceptionIsThrownAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IMachineService>();
            var watchId = "watch-id-1";

            //When
            mockService.Setup(ms => ms.GetAllMachinesWithSubs(It.IsAny<string>())).Throws<InvalidDataException>();

            var res = (BadRequestObjectResult) await new GetMachinesWithSubs(mockService.Object).Run(req, logger, watchId);
            
            //Then
            Assert.IsType<BadRequestObjectResult>(res);
        }

        [Fact]
        public async Task TestFunctionShouldIncludeErrorMessageInBadRequestObjectResultAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var mockService = new Mock<IMachineService>();
            var watchId = "watch-id-1";

            //When
            mockService.Setup(ms => ms.GetAllMachinesWithSubs(It.IsAny<string>())).Throws<InvalidDataException>();

            var res = (BadRequestObjectResult) await new GetMachinesWithSubs(mockService.Object).Run(req, logger, watchId);
            
            //Then
            Assert.False(String.IsNullOrEmpty((string) res.Value));
        }
    }
}