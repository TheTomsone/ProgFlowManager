using ProgFlowManager.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Models.Users
{
    public class UserDTO : DataDTO, IModelDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public IEnumerable<TeamDTO> Teams { get; set; }
        public string Fullname => Firstname + ' ' + Lastname;
    }
}
