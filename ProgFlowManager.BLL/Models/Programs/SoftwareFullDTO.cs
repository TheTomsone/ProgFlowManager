using ProgFlowManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Models.Programs
{
    public class SoftwareFullDTO : SoftwareDTO, IModel
    {
        public List<VersionNbFullDTO> Versions { get; set; }

        public SoftwareFullDTO() : base()
        {
            Versions = new List<VersionNbFullDTO>();
        }
    }
}
