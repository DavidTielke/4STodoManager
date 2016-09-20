using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebClient.Models;

namespace WebClient.Services
{
    public class UserManager : IUserManager
    {
        private readonly IRepository<User> _repository;

        public UserManager(IRepository<User> repository )
        {
            _repository = repository;
        }

        public bool ValidateUser(string username, string password)
        {
            var user = GetUser(username);

            if (user?.Password == password)
            {
                return true;
            }
            return false;
        }

        public User GetUser(string username)
        {
            var user = _repository.Load().SingleOrDefault(u => u.Username == username);
            return user;
        }

        public string[] GetClaims(string username)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUserManager
    {
        bool ValidateUser(string username, string password);
        string[] GetClaims(string username);
    }
}