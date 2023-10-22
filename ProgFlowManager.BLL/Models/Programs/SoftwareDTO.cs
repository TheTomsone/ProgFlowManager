using ProgFlowManager.DAL.Interfaces;
using ProgFlowManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Models.Programs
{
    public class SoftwareDTO : Data, IModel
    {
        public DateTime ETA { get; set; }
        public DateTime Started { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public List<LanguageDTO> Languages { get; set; }

        public SoftwareDTO()
        {
            Categories = new List<CategoryDTO>();
            Languages = new List<LanguageDTO>();
        }
    }
}
