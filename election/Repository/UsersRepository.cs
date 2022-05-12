using System;
using System.Collections.Generic;
using System.Linq;
using election.Models;

namespace election.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private Dictionary<string, string> userRepository = new Dictionary<string, string>();
        public UsersRepository()
        {
            userRepository.Add("test", "test");
        }

        void IUsersRepository.AddNewUser(Users user)
        {
            userRepository.Add(user.user, user.password);
        }

        bool IUsersRepository.Contain(string user)
        {
            return userRepository.Any(x => x.Key == user);
        }

        bool IUsersRepository.Verify(Users user)
        {
            return userRepository.Any(x => x.Key == user.user && x.Value == user.password);
        }

        
    }
}
