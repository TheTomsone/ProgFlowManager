using ProgFlowManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Models.Programs
{
    public class VersionNbFullDTO : VersionNbDTO, IModel
    {
        public List<ContentDTO> Contents { get; set; }

        public VersionNbFullDTO() : base()
        {
            Contents = new List<ContentDTO>();
        }
    }
}
