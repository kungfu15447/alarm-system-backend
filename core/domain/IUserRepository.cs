using System;
using System.Collections.Generic;
using Core.Entity.DB;

namespace AlarmSystem.Core.Domain
{
    public interface IUserRepository
    {
         User GetUserByGuid(string guid);
         User GetUserByName(string name);
         User GetUserByEmail(string email);
         List<User> GetUsers();
         void CreateUser(User user);
    }
}