using Microsoft.Extensions.Configuration;
using ProgFlowManager.DAL.Interfaces;
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
    public class SoftwareService : Creatable<Software>, ISoftwareService
    {
        private readonly IDataService _dataService;
        public SoftwareService(IConfiguration config, IDataService dataService) : base(config)
        {
            _dataService = dataService;
        }

        public override IEnumerable<Software> GetAll()
        {
            return base.GetAll().MergeWith(_dataService.GetAll(), software => software.Id, data => data.Id);
        }
    }
}
