using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProgFlowManager.DAL.Interfaces;
using ProgFlowManager.DAL.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Services.Base
{
    public class Modifiable<TModel> : Readable<TModel>, IModifiable<TModel> where TModel : class
    {
        public Modifiable(IConfiguration config) : base(config)
        {
        }

        public bool Delete(int id)
        {
            using SqlCommand cmd = Connection.CreateCommand();
            PropertyInfo propId = typeof(TModel).GetProperties()[0];
            string sql = $"DELETE FROM [dbo].[{FullTablename}] WHERE [{Prefix.ToLower()}_{propId.Name.UnderscoreBetweenLowerUpper().ToLower()}] = @id";

            cmd.Parameters.AddWithValue("id", id);
            cmd.CommandText = sql;

            Connection.Open();

            try { return cmd.ExecuteNonQuery() > 0; }
            finally { Connection.Close(); }
        }
        public bool Update(TModel model)
        {
            using SqlCommand cmd = Connection.CreateCommand();
            PropertyInfo[] props = typeof(TModel).GetProperties();
            PropertyInfo propId = props[0];
            StringBuilder sb = new($"UPDATE [dbo].[{FullTablename}] SET ");

            for (int i = 1; i < props.Length; i++)
            {
                sb.Append($"[{Prefix}_{props[i].Name.UnderscoreBetweenLowerUpper().ToLower()}] = @{props[i].Name}");
                cmd.Parameters.AddWithValue(props[i].Name, props[i].GetValue(model));

                if (i < props.Length - 1) sb.Append(',');

            }
            sb.Append($" WHERE {Prefix.ToLower()}_{propId.Name.UnderscoreBetweenLowerUpper().ToLower()} = @{propId.Name}");
            cmd.Parameters.AddWithValue(propId.Name, propId.GetValue(model));
            cmd.CommandText = sb.ToString();

            Connection.Open();

            try { return cmd.ExecuteNonQuery() > 0; }
            finally { Connection.Close(); }
        }
    }
}
