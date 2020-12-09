
using AlarmSystem.Core.Domain;
using Core.Entity.DB;
using Core.Entity.Dto.Authentication;

namespace AlarmSystem.Core.Application.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        public IUserRepository _userRepo;
        public IAuthenticationHelper _authenticationHelper;
        public AuthenticationService(IAuthenticationHelper authenticationHelper, IUserRepository userRepository)
        {
            _authenticationHelper = authenticationHelper;
            _userRepo = userRepository;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            _authenticationHelper.CreatePasswordHash(password,  out passwordHash, out passwordSalt);
        }
        public string GenerateToken(User user)
        {
            return _authenticationHelper.GenerateToken(user);
        }
        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            return _authenticationHelper.VerifyPasswordHash( password,  storedHash, storedSalt);
        }
    }
}