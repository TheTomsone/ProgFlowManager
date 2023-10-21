﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Interfaces.Base
{
    public interface ICreatable<TModel> : IModifiable<TModel> where TModel : class
    {
        bool Create(TModel model);
        int GetLastId();
    }
}
