using System;
using election.Models;

namespace election.Repository
{
    public interface IUsersRepository
    {
        public void AddNewUser(Users user);
        public bool Contain(string user);
        public bool Verify(Users user);
    }
}
