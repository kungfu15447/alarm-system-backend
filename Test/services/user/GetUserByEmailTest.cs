using System.IO;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.User
{
    public class GetUserByEmailTest
    {
        [Fact]
        public void GetAllUsersFromUserRepoShouldBeCalledOnce()
        {
            //Given
            var userRepo = new Mock<IUserRepository>();
            var authHelper = new Mock<IAuthenticationHelper>();

            byte[] passwordHash;
            byte[] passwordSalt;
            var name = "Tommy";

            //When
            userRepo.Setup(ur => ur.GetUserByEmail(It.IsAny<string>())).Returns(It.IsAny<AlarmSystem.Core.Entity.DB.User>());
            authHelper.Setup(ah => ah.CreatePasswordHash(It.IsAny<string>(), out passwordHash, out passwordSalt));
            var userService = new UserService(userRepo.Object, authHelper.Object);

            userService.GetUserByEmail(name);

            //Then
            userRepo.Verify(ur => ur.GetUserByEmail(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestRepoShouldNotBeCalledIfWatchIdIsNullOrEmpty(string watchId)
        {
            //Given
            var userRepo = new Mock<IUserRepository>();
            var authHelper = new Mock<IAuthenticationHelper>();

            byte[] passwordHash;
            byte[] passwordSalt;

            //When
            userRepo.Setup(ur => ur.GetUserByEmail(It.IsAny<string>())).Returns(It.IsAny<AlarmSystem.Core.Entity.DB.User>());
            authHelper.Setup(ah => ah.CreatePasswordHash(It.IsAny<string>(), out passwordHash, out passwordSalt));

            var userService = new UserService(userRepo.Object, authHelper.Object);
        
            //Then
            Assert.Throws<InvalidDataException>(() => userService.GetUserByEmail(It.IsAny<string>()));
            userRepo.Verify(ur => ur.GetUserByEmail(It.IsAny<string>()), Times.Never);
        }
    }
}