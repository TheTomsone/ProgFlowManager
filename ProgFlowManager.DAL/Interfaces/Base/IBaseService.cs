﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Interfaces.Base
{
    public interface IBaseService<TModel> where TModel : class
    {
        string Prefix { get; }
        string Tablename { get; }
        string FullTablename { get; }
        SqlConnection Connection { get; }
        TModel Map(IDataReader reader);
    }
}
