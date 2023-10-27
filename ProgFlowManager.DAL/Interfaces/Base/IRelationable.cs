using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Interfaces.Base
{
    public interface IRelationable<TModel> : ICreatable<TModel> where TModel : class
    {
        IEnumerable<TModel> GetAllById(int id, string relation);
        bool DeleteRelation(TModel model);
    }
}
