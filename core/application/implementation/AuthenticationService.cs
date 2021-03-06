using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.DB;

namespace AlarmSystem.Core.Application.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        public IAuthenticationHelper _authenticationHelper;
        public AuthenticationService(IAuthenticationHelper authenticationHelper)
        {
            _authenticationHelper = authenticationHelper;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            _authenticationHelper.CreatePasswordHash(password,  out passwordHash, out passwordSalt);
        }

        public bool DecryptToken(string token)
        {
            return _authenticationHelper.DecryptToken(token);
        }

        public string GenerateToken(User user)
        {
            return _authenticationHelper.GenerateToken(user);
        }
        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            return _authenticationHelper.VerifyPasswordHash(password,  storedHash, storedSalt);
        }
    }
}