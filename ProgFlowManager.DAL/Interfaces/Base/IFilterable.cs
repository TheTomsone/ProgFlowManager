using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Interfaces.Base
{
    public interface IFilterable<TModel> : ICreatable<TModel> where TModel : class
    {
        IEnumerable<TModel> FilterBy(string[] propNames, object[] values);
    }
}
