using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
    public class Readable<TModel> : BaseService<TModel>, IReadable<TModel> where TModel : class
    {
        public IEnumerable<TModel> Models => GetAll();

        public Readable(IConfiguration config) : base(config)
        {
        }

        public virtual IEnumerable<TModel> GetAll()
        {
            using SqlCommand cmd = Connection.CreateCommand();
            List<TModel> list = new();
            string sql = $"SELECT * FROM [dbo].[{FullTablename}]";

            cmd.CommandText = sql;

            Connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows) while (reader.Read()) list.Add(Map(reader));

            try { return list; }
            finally { Connection.Close(); }
        }

        public TModel GetById(int id)
        {
            using SqlCommand cmd = Connection.CreateCommand();
            string sql = $"SELECT * FROM [dbo].[{FullTablename}] WHERE [{Prefix.ToLower()}_id] = @id";

            cmd.Parameters.AddWithValue("id", id);
            cmd.CommandText = sql;

            Connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            try
            {
                if (reader.Read()) return Map(reader);
                else return Activator.CreateInstance<TModel>();
            }
            finally { Connection.Close(); }
        }
    }
}
