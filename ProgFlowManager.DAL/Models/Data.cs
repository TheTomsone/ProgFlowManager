using ProgFlowManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Models
{
    public class Data : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Resume { get; set; }
        public DateTime Created { get; set;}
        public DateTime? Updated { get; set;}
        public byte[]? ImageData { get; set; }
        public string? ImageMime { get; set; }
    }
}
