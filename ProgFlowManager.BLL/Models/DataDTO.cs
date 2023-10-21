using ProgFlowManager.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Models
{
    public class DataDTO : IModelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Resume { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageMime { get; set; }
    }
}
