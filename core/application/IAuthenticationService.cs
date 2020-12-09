using Core.Entity.DB;
using Core.Entity.Dto.Authentication;

namespace AlarmSystem.Core.Application
{
    public interface IAuthenticationService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        string GenerateToken(User user);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}