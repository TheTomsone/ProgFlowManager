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
    public class Creatable<TModel> : Modifiable<TModel>, ICreatable<TModel> where TModel : class
    {
        public Creatable(IConfiguration config) : base(config)
        {
        }

        public virtual bool Create(TModel model)
        {
            using SqlCommand cmd = Connection.CreateCommand();
            StringBuilder sb = new();
            PropertyInfo[] props = typeof(TModel).GetProperties();
            props = RemoveNullValue(ref props, model);

            sb.Append($"INSERT INTO [dbo].[{FullTablename}] ("); 
            for (int i = 0; i < props.Length; i++)
            {
                if (Tablename == "DATA" && props[i].Name == "Id") continue;
                if (props[i].GetValue(model) is null) continue;

                sb.Append($"[{Prefix.ToLower()}_{props[i].Name.UnderscoreBetweenLowerUpper().ToLower()}]");
                if (i < props.Length - 1) sb.Append(',');
            }
            sb.Append(") VALUES (");
            for (int i = 0; i < props.Length; i++)
            {
                if (Tablename == "DATA" && props[i].Name == "Id") continue;
                if (props[i].GetValue(model) is null) continue;

                sb.Append($"@{props[i].Name.ToString()}");
                cmd.Parameters.AddWithValue(props[i].Name.ToString(), props[i].GetValue(model));
                if (i < props.Length - 1) sb.Append(',');
                

            }
            sb.Append(')');

            cmd.CommandText = sb.ToString();

            Connection.Open();
            try { return cmd.ExecuteNonQuery() > 0; }
            finally { Connection.Close(); }
        }

        public int GetLastId()
        {
            using SqlCommand cmd = Connection.CreateCommand();
            string sql = $"SELECT IDENT_CURRENT('{FullTablename}')";

            cmd.CommandText = sql;

            Connection.Open();
            try { return Convert.ToInt32(cmd.ExecuteScalar()); }
            finally { Connection.Close(); }
        }

        private PropertyInfo[] RemoveNullValue(ref PropertyInfo[] props, TModel model)
        {
            return props.Where(p => p.GetValue(model) is not null).ToArray();
        }
    }
}
