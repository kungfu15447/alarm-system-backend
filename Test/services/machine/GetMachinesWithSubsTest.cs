using System.Collections.Generic;
using AlarmSystem.Core.Domain;
using Moq;
using Xunit;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Core.Application.Implementation;
using System.IO;
using System;

namespace AlarmSystem.Test.Services.Machine
{
    public class GetMachinesWithSubsTest
    {
        [Fact]
        public void MethodShouldCallGetMachinesWithSubsMethodFromRepoOnce()
        {
            //Given
            var mockRepo = new Mock<IMachineRepository>();
            var service = new MachineService(mockRepo.Object);
            var watchId = "watch-id-1";

            //When
            mockRepo.Setup(mr => mr.ReadAllMachinesWithSubs(It.IsAny<string>())).Returns(It.IsAny<List<MachineWithSubscription>>());

            service.GetAllMachinesWithSubs(watchId);
        
            //Then
            mockRepo.Verify(mr => mr.ReadAllMachinesWithSubs(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestRepoShouldNotBeCalledIfWatchIdIsNullOrEmpty(string watchId)
        {
            //Given
            var mockRepo = new Mock<IMachineRepository>();
            var service = new MachineService(mockRepo.Object);

            //When
            mockRepo.Setup(mr => mr.ReadAllMachinesWithSubs(It.IsAny<string>())).Returns(It.IsAny<List<MachineWithSubscription>>());
        
            //Then
            Assert.Throws<InvalidDataException>(() => service.GetAllMachinesWithSubs(It.IsAny<string>()));
            mockRepo.Verify(mr => mr.ReadAllMachinesWithSubs(It.IsAny<string>()), Times.Never);
        }
    }
}