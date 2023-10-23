using ProgFlowManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Models.Programs
{
    public class VersionNb : IModel
    {
        public int Id { get; set; }
        public int Major {  get; set; }
        public int Minor { get; set; }
        public int Patch { get; set; }
        public DateTime? Goal {  get; set; }
        public DateTime? Release {  get; set; }
        public int StageId { get; set; }
        public int SoftwareId { get; set; }
    }
}
