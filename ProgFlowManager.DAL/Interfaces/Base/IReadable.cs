using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Interfaces.Base
{
    public interface IReadable<TModel> : IBaseService<TModel> where TModel : class
    {
        IEnumerable<TModel> Models { get; }
        TModel GetById(int id);
        IEnumerable<TModel> GetAll();
    }
}
