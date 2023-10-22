using ProgFlowManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Models.Programs
{
    public class Content : IModel
    {
        public int Id { get; set; }
        public int VersionNbId { get; set; }
        public int StageId { get; set; }
    }
}
