﻿using ProgFlowManager.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Models.Programs
{
    public class SoftwareDTO : DataDTO, IModelDTO
    {
        public DateTime ETA { get; set; }
        public DateTime Started { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
        public IEnumerable<LanguageDTO> Languages { get; set; }
    }
}
