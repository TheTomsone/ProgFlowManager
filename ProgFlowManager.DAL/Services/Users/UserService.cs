using Microsoft.Extensions.Configuration;
using ProgFlowManager.DAL.Interfaces.Users;
using ProgFlowManager.DAL.Models.Users;
using ProgFlowManager.DAL.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Services.Users
{
    public class UserService : Creatable<User>, IUserService
    {
        public UserService(IConfiguration config) : base(config)
        {
        }

        public User Login(string email)
        {
            return Models.First(u => u.Email == email);
        }
        public bool Register(User user)
        {
            return Create(user);
        }
    }
}
