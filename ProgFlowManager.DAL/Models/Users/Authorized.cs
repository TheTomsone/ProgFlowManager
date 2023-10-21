using ProgFlowManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Models.Users
{
    public class Authorized : IModelDAL
    {
        public int Id { get; set; }
        public bool Modifiable { get; set; }
        public bool Creatable { get; set; }

    }
}
