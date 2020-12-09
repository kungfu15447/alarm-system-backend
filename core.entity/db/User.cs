using System;

namespace Core.Entity.DB
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}