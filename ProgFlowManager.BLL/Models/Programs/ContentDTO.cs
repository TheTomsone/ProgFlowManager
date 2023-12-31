﻿using ProgFlowManager.DAL.Interfaces;
using ProgFlowManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.BLL.Models.Programs
{
    public class ContentDTO : Data, IModel
    {
        public int VersionNbId { get; set; }
        public StageDTO Stage { get; set; }

        public ContentDTO()
        {
            Stage = new StageDTO();
        }
    }
}
