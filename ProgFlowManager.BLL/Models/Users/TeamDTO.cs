using ProgFlowManager.DAL.Interfaces;
using ProgFlowManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Models.Users
{
    public class TeamDTO : Data, IModel
    {
        public TeamCategoryDTO TeamCategories { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }
    }
}
