using ProgFlowManager.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Models.Users
{
    public class AuthorizedDTO : IModelDTO
    {
        public int Id { get; set; }
        public bool Modifiable { get; set; }
        public bool Creatable { get; set; }
    }
}
