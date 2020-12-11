using System.Collections.Generic;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.User
{
    public class GetUsersTest
    {
        [Fact]
        public void GetAllUsersFromUserRepoShouldBeCalledOnce()
        {
            //Given
            var userRepo = new Mock<IUserRepository>();
            var authHelper = new Mock<IAuthenticationHelper>();

            byte[] passwordHash;
            byte[] passwordSalt;

            //When
            userRepo.Setup(ur => ur.GetUsers()).Returns(It.IsAny<List<AlarmSystem.Core.Entity.DB.User>>());
            authHelper.Setup(ah => ah.CreatePasswordHash(It.IsAny<string>(), out passwordHash, out passwordSalt));
            var userService = new UserService(userRepo.Object, authHelper.Object);

            userService.GetUsers();
            
            //Then
            userRepo.Verify(ur => ur.GetUsers(), Times.Once);
        }
    }
}