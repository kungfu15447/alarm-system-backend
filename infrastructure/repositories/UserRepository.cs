using System;
using System.Collections.Generic;
using System.Linq;
using AlarmSystem.Core.Domain;
using Core.Entity.DB;

namespace AlarmSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private SystemContext _ctx;
        public UserRepository(SystemContext ctx){
            _ctx = ctx;
        }
        public void CreateUser(User user)
        {
            
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
        }

        public User GetUserByEmail(string email)
        {
            return _ctx.Users.FirstOrDefault(user => user.Email == email);
        }

        public User GetUserByName(string name)
        {
            return _ctx.Users.FirstOrDefault(user => user.Name == name);
        }

        public User GetUserByGuid(string guid)
        {
            return _ctx.Users.FirstOrDefault(user => user.UserId == guid);
        }

        public List<User> GetUsers()
        {
            return _ctx.Users.ToList();
        }
    }
}