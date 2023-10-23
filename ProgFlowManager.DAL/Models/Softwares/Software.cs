using ProgFlowManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Models.Programs
{
    public class Software : IModel
    {
        public int Id { get; set; }
        public DateTime? ETA { get; set; }
        public DateTime Started { get; set; }
        public int? UserId { get; set; }
        public int? TeamId { get; set; }
    }
}
