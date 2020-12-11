using AlarmSystem.Core.Application;
using AlarmSystem.Core.Entity.Dto;
using AlarmSystem.Functions.User;
using AlarmSystem.Test.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.User 
{
    public class CreateUserTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void FunctionShouldCallUserServiceOnce()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var userService = new Mock<IUserService>();

            //When
            userService.Setup(us => us.CreateUser(It.IsAny<UserToCreate>()));

            var res = await new CreateUser(userService.Object).Run(req, logger);

            //Then
            userService.Verify(us => us.CreateUser(It.IsAny<UserToCreate>()), Times.Once());
        
        }

        [Fact]
        public async void FucntionShouldReturnOkResult()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var userService = new Mock<IUserService>();

            //When
            userService.Setup(us => us.CreateUser(It.IsAny<UserToCreate>()));
            
            var res = (OkResult)await new CreateUser(userService.Object).Run(req, logger);

            //Then
            Assert.IsType<OkResult>(res);
        }
    }
}