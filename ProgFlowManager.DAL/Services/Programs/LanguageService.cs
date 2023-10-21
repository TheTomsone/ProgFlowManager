﻿using Microsoft.Extensions.Configuration;
using ProgFlowManager.DAL.Interfaces.Programs;
using ProgFlowManager.DAL.Models.Programs;
using ProgFlowManager.DAL.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Services.Programs
{
    public class LanguageService : Readable<Language>, ILanguageService
    {
        public LanguageService(IConfiguration config) : base(config)
        {
        }
    }
}
