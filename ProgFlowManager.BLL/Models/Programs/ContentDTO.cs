using ProgFlowManager.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Models.Programs
{
    public class ContentDTO : DataDTO, IModelDTO
    {
        public StageDTO Stage { get; set; }
    }
}
