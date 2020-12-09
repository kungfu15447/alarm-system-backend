using System;
using System.Collections.Generic;
using AlarmSystem.Core.Entity.Dto;
using Core.Entity.DB;

namespace AlarmSystem.Core.Application
{
    public interface IUserService
    {
         User GetUserByGuid(string guid);
         User GetUserByEmail(string email);
         List<User> GetUsers();
         void CreateUser(UserToCreate user);
         User GetUserByName(string name);
    }
}