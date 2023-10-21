using ProgFlowManager.DAL.Interfaces.Base;
using ProgFlowManager.DAL.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Interfaces.Users
{
    public interface IUserService : ICreatable<User>
    {
        bool Register(User user);
        User Login(string email);
    }
}
