using System.Collections.Generic;
using System.Threading.Tasks;
using AlarmSystem.Core.Application;
using AlarmSystem.Functions.User;
using AlarmSystem.Test.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Functions.User
{
    public class GetUsersTest
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void FunctionShouldCallUserServiceOnce() {
            //Given
            var req = new HttpRequestBuilder().Build();
            var userService = new Mock<IUserService>();

            var listToReturn = new List<AlarmSystem.Core.Entity.DB.User>() {
                new AlarmSystem.Core.Entity.DB.User(){
                    Name = "Tom",
                    PasswordHash = new byte[5],
                    PasswordSalt = new byte[5],
                    UserId = "GUID",
                    Email = "tom@google.dk"
                }
            };

            //When
            userService.Setup(us => us.GetUsers()).Returns(listToReturn);

            var res = await new GetUsers(userService.Object).Run(req, logger);

            //Then
            userService.Verify(us => us.GetUsers(), Times.Once());
        }

        [Fact]
        public async void FunctionShouldReturnOkObjectResult()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var userService = new Mock<IUserService>();

            var listToReturn = new List<AlarmSystem.Core.Entity.DB.User>() {
                new AlarmSystem.Core.Entity.DB.User(){
                    Name = "Tom",
                    PasswordHash = new byte[5],
                    PasswordSalt = new byte[5],
                    UserId = "GUID",
                    Email = "tom@google.dk"
                }
            };

            //When
            userService.Setup(us => us.GetUsers()).Returns(listToReturn);

            var res = (OkObjectResult) await new GetUsers(userService.Object).Run(req, logger);

            //Then
            Assert.IsType<OkObjectResult>(res);
        }

        [Fact]
        public async Task TestFunctionShouldReturnNoContentResultIfListIsEmptyAsync()
        {
            //Given
            var req = new HttpRequestBuilder().Build();
            var userService = new Mock<IUserService>();
            
            List<AlarmSystem.Core.Entity.DB.User> noUsers = new List<AlarmSystem.Core.Entity.DB.User>();
        
            //When
            userService.Setup(us => us.GetUsers()).Returns(noUsers);

            var res = (NoContentResult) await new GetUsers(userService.Object).Run(req, logger);
            
            //Then
            Assert.IsType<NoContentResult>(res);
        }
    }
}