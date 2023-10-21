using ProgFlowManager.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Models.Programs
{
    public class VersionNbDTO : DataDTO, IModelDTO
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Patch { get; set; }
        public DateTime Goal { get; set; }
        public DateTime Release { get; set; }
        public StageDTO Stage { get; set; }
        public SoftwareDTO Software { get; set; }
    }
}
