using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Demo.Core.Data.DbContext
{
    public class SqlParameterFactory
    {
        private readonly IConfiguration configuration;

        public SqlParameterFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbParameter CreateParameter(string parameterName, object value)
        {
            if (configuration.GetValue<bool>("UsePostgres"))
            {
                return new NpgsqlParameter(parameterName, value);
            }
            else
            {
                return new SqlParameter(parameterName, value);
            }
        }

        public DbParameter CreateParameterForStringComparison(string parameterName, object value)
        {
            if (configuration.GetValue<bool>("UsePostgres"))
            {
                var param = new NpgsqlParameter();
                param.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;  
                param.ParameterName = parameterName;
                
                if (value == DBNull.Value)
                {
                    param.NpgsqlValue = DBNull.Value;
                }
                else
                {
                    param.NpgsqlValue = string.Format("%{0}%", value);
                }
                
                return param;
            }
            else
            {
                return new SqlParameter(parameterName, value);
            }
        }
    }
}
