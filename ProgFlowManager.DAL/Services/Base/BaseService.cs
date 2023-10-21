using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProgFlowManager.DAL.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProgFlowManager.DAL.Services.Base
{
    public class BaseService<TModel> : IBaseService<TModel> where TModel : class
    {
        private readonly SqlConnection _connection;

        public string Prefix => typeof(TModel).Name.DeleteLower();
        public string Tablename => typeof(TModel).Name.UnderscoreBetweenLowerUpper().ToUpper();
        public string FullTablename => Prefix + "_" + Tablename;
        public SqlConnection Connection => _connection;

        public BaseService(IConfiguration config)
        {
            _connection = new SqlConnection(config.GetConnectionString("default"));
        }

        public TModel Map(IDataReader reader)
        {
            TModel model = Activator.CreateInstance<TModel>();
            PropertyInfo[] props = typeof(TModel).GetProperties();

            foreach (PropertyInfo prop in props)
            {
                object value = reader[$"{Prefix.ToLower()}_{prop.Name.UnderscoreBetweenLowerUpper().ToLower()}"];
                if (value is not DBNull && value is not null) prop.SetValue(model, value);
            }

            return model;
        }
    }
    public static partial class RegexConverter
    {
        [GeneratedRegex("[a-z]")]
        private static partial Regex RegexDeleteLowerChar();
        [GeneratedRegex("([a-z])([A-Z])")]
        private static partial Regex RegexUnderscoreBetweenLowerUpper();

        public static string UnderscoreBetweenLowerUpper(this string str)
        {
            return RegexUnderscoreBetweenLowerUpper().Replace(str, "$1_$2");
        }
        public static string DeleteLower(this string str)
        {
            return RegexDeleteLowerChar().Replace(str, "");
        }
    }
}
