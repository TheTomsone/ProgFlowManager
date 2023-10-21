using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Interfaces.Base
{
    public interface IRelationable<TModel> : ICreatable<TModel> where TModel : class
    {
        IEnumerable<TModel> GetAllById<TRelation>(int id) where TRelation : class;
        bool DeleteRelation(TModel model);
    }
}
