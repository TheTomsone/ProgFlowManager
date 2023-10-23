using Microsoft.Extensions.Configuration;
using ProgFlowManager.DAL.Interfaces;
using ProgFlowManager.DAL.Models;
using ProgFlowManager.DAL.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Services
{
    public class DataService : Creatable<Data>, IDataService
    {
        public DataService(IConfiguration config) : base(config)
        {
        }

        //public override bool Create(Data model)
        //{
        //    model.Created = DateTime.Now;
        //    model.Updated = DateTime.Now;
        //    return base.Create(model);
        //}
    }
}
