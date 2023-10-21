using ProgFlowManager.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Models.Users
{
    public class TeamDTO : DataDTO, IModelDTO
    {
        public TeamCategoryDTO TeamCategories { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }
    }
}
