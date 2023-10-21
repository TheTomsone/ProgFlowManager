using ProgFlowManager.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Models.Users
{
    public class RoleDTO : TeamCategoryDTO, IModelDTO
    {
        public AuthorizedDTO Authorized { get; set; }
    }
}
