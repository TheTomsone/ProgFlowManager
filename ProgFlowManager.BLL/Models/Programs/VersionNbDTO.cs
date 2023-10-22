using ProgFlowManager.DAL.Interfaces;
using ProgFlowManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Models.Programs
{
    public class VersionNbDTO : Data, IModel
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Patch { get; set; }
        public DateTime Goal { get; set; }
        public DateTime Release { get; set; }
        public StageDTO Stage { get; set; }
        public int SoftwareId { get; set; }

        public VersionNbDTO()
        {
            Stage = new StageDTO();
        }
    }
}
