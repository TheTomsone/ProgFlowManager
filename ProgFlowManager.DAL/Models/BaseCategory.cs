using ProgFlowManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Models
{
    public abstract class BaseCategory : IModelDAL
    {
        public int Id { get; set; }
        public string Label { get; set; }
    }
}
