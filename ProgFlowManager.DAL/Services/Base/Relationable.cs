using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProgFlowManager.DAL.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Services.Base
{
    public class Relationable<TModel> : Creatable<TModel>, IRelationable<TModel> where TModel : class
    {
        public Relationable(IConfiguration config) : base(config)
        {
        }

        public bool DeleteRelation(TModel model)
        {
            using SqlCommand cmd = Connection.CreateCommand();
            StringBuilder sb = new($"DELETE FROM [dbo].[{FullTablename}] WHERE ");
            PropertyInfo[] props = typeof(TModel).GetProperties();

            for (int i = 0; i < props.Length; i++)
            {
                if (i is not 0) sb.Append(" AND ");

                sb.Append($"[{Prefix.ToLower()}_{props[i].Name.UnderscoreBetweenLowerUpper().ToLower()}] = @{props[i].Name}");
                cmd.Parameters.AddWithValue(props[i].Name, props[i].GetValue(model));
            }

            cmd.CommandText = sb.ToString();

            Connection.Open();
            try { return cmd.ExecuteNonQuery() > 0; }
            finally { Connection.Close(); }
        }

        public IEnumerable<TModel> GetAllById(int id, string relation)
        {
            using SqlCommand cmd = Connection.CreateCommand();
            List<TModel> list = new();
            string sql = $"SELECT * FROM [dbo].[{FullTablename}] WHERE [{Prefix.ToLower()}_{relation.UnderscoreBetweenLowerUpper().ToLower()}_id] = @id";

            cmd.Parameters.AddWithValue("id", id);
            cmd.CommandText = sql;

            Connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) list.Add(Map(reader));

            try { return list; }
            finally { Connection.Close(); }
        }
    }
}
