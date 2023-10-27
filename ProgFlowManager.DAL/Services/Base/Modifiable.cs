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

            foreach (PropertyInfo prop in props)
            {
                Console.WriteLine(prop.Name + " - " + prop.GetValue(model));
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            props = props.Where(prop =>
            {
                var value = prop.GetValue(model);

                if (prop.Name == "Id") return false;
                if (value is null) return false;
                if (value is string stringValue && string.IsNullOrEmpty(stringValue)) return false;
                if (value is int intValue && intValue == 0)return false;

                return true;
            }).ToArray();

            if (props.Length < 1) return true;

            for (int i = 0; i < props.Length; i++)
            {
                sb.Append($"[{Prefix}_{props[i].Name.UnderscoreBetweenLowerUpper().ToLower()}] = @{props[i].Name}");
                cmd.Parameters.AddWithValue(props[i].Name, props[i].GetValue(model));

                if (i < props.Length - 1) sb.Append(',');

            }
            sb.Append($" WHERE {Prefix.ToLower()}_{propId.Name.UnderscoreBetweenLowerUpper().ToLower()} = @{propId.Name}");

            Console.WriteLine(sb.ToString());

            cmd.Parameters.AddWithValue(propId.Name, propId.GetValue(model));
            cmd.CommandText = sb.ToString();

            Connection.Open();

            try { return cmd.ExecuteNonQuery() > 0; }
            finally { Connection.Close(); }
        }
    }
}
