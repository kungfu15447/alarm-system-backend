using AlarmSystem.Core.Application;
using AlarmSystem.Core.Application.Implementation;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using Moq;
using Xunit;

namespace AlarmSystem.Test.Services.User
{
    public class CreateUserTest
    {
        [Fact]
        public void MethodShouldCallCreateUserMethodFromRepoOnce()
        {
            //Given
            var authHelper = new Mock<IAuthenticationHelper>();
            var userRepo = new Mock<IUserRepository>();

            byte[] passwordHash; 
            byte[] passwordSalt; 

            //When
            userRepo.Setup(mr => mr.CreateUser(It.IsAny<AlarmSystem.Core.Entity.DB.User>()));
            authHelper.Setup(mr => mr.CreatePasswordHash(It.IsAny<string>(), out  passwordHash, out passwordSalt));

            var userToCreate = new UserToCreate {
                Name = "Tom",
                Email = "tom@google.com",
                Password = "1234"
            };
            var userService = new UserService(userRepo.Object, authHelper.Object);
            userService.CreateUser(userToCreate);

            //Then
            userRepo.Verify(mr => mr.CreateUser(It.IsAny<AlarmSystem.Core.Entity.DB.User>()), Times.Once);
        }
    }
}