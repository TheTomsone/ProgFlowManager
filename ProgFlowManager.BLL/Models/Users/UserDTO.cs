using ProgFlowManager.DAL.Interfaces;
using ProgFlowManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Models.Users
{
    public class UserDTO : Data, IModel
    {
        public string Firstname { private get; set; }
        public string Lastname { private get; set; }
        public string Email { get; set; }
        public IEnumerable<TeamDTO> Teams { get; set; }
        public string Fullname => Firstname + ' ' + Lastname;
    }
}
