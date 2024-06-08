using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SQLite;

namespace EnscryptDb.Core
{
    public class DbConnectionFactory : IConnectionFactory
    {

        private readonly SQLiteFactory _provider;
        private readonly string _connectionString;
        private readonly string _name;

        public DbConnectionFactory(string connectionName)
        {
            if (connectionName == null) throw new ArgumentNullException("connectionName");

            var conString = ConfigurationManager.ConnectionStrings[connectionName];
            if (conString == null)
                throw new ConfigurationErrorsException(string.Format("Failed to find connection string named '{0}' in app.config.", connectionName));

            _name = conString.ProviderName;
            _provider = new  SQLiteFactory();
            _connectionString = conString.ConnectionString;

        }

        public IDbConnection Create()
        {
            try
            {
                var connection = _provider.CreateConnection();
                if (connection == null)
                    throw new ConfigurationErrorsException(string.Format("Failed to create a connection using the connection string named '{0}' in app.config.", _name));

                connection.ConnectionString = _connectionString;
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
