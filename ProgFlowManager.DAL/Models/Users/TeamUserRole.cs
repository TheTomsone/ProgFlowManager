using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Models.Users
{
    public class TeamUserRole
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
