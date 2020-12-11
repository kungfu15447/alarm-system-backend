using System;
using System.Collections.Generic;
using AlarmSystem.Core.Entity.DB;
using AlarmSystem.Core.Entity.Dto;

namespace AlarmSystem.Core.Application
{
    public interface IUserService
    {
         User GetUserByEmail(string email);
         List<User> GetUsers();
         void CreateUser(UserToCreate user);
        User GetUserByName(string name);
    }
}