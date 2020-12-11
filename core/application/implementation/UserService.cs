using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using AlarmSystem.Core.Domain;
using AlarmSystem.Core.Entity.DB;
using AlarmSystem.Core.Entity.Dto;

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
            if(user != null){
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
            else{
                throw new InvalidDataException("User id cannot be empty or non existent! Please include a user id");
            }
        }

    

        public User GetUserByEmail(string email)
        {
            if(!string.IsNullOrEmpty(email))
            {
                return _userRepo.GetUserByEmail(email);
            }
            else 
            {
                throw new InvalidDataException("Email cannot be empty or non existent! Please include a email");
            }
        }

        public User GetUserByName(string name)
        {
            if(!string.IsNullOrEmpty(name)){
                return _userRepo.GetUserByName(name);
            }else {
                throw new InvalidDataException("Email cannot be empty or non existent! Please include a email");
            }
            
        }

        public List<User> GetUsers()
        {
            return _userRepo.GetUsers();
        }
    }
}