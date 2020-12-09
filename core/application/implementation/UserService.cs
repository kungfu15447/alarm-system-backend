using System;
using System.Collections;
using System.Collections.Generic;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.Dto;
using Core.Entity.DB;

namespace AlarmSystem.Core.Application.Implementation
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepo;
        public IAuthenticationHelper _authHelper;
        public UserService(IUserRepository userRepo, IAuthenticationHelper authHelper){
            _userRepo = userRepo;
            _authHelper = authHelper;
        }
        public void CreateUser(UserToCreate user)
        {
            
                if(_userRepo.GetUserByEmail(user.Email) == null)
                {
                byte[] passwordHash;
                byte[] passwordSalt;
                _authHelper.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
                var guid = Guid.NewGuid();
                _userRepo.CreateUser(new User{
                    UserId = guid.ToString(),
                    Name = user.Name,
                    Email = user.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt 
                });
            }
        }

        public User GetUserByEmail(string email)
        {
            return _userRepo.GetUserByEmail(email);
        }

        public User GetUserByGuid(string guid)
        {
            return _userRepo.GetUserByGuid(guid);
        }

        public User GetUserByName(string name)
        {
            return _userRepo.GetUserByName(name);
        }

        public List<User> GetUsers()
        {
            return _userRepo.GetUsers();
        }
    }
}