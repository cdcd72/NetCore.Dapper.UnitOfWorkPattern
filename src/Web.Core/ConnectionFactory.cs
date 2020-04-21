using Microsoft.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Web.Core.Configuration;
using Web.Core.Enum;
using Web.Core.Interfaces;

namespace Web.Core
{
    public class ConnectionFactory : IConnectionFactory
    {
        #region Properties

        private readonly IDbSettingsResolved _dbSettingsResolved;

        #endregion

        #region Constructor

        public ConnectionFactory(IDbSettingsResolved dbSettingsResolved)
        {
            _dbSettingsResolved = dbSettingsResolved;
        }

        #endregion

        /// <summary>
        /// 取得資料庫連線
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            return CreateConnection(_dbSettingsResolved.ConnectionType, _dbSettingsResolved.ConnectionString);
        }

        #region Private Method

        /// <summary>
        /// 建立資料庫連線
        /// </summary>
        /// <param name="dbProvider"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private IDbConnection CreateConnection(DBProvider dbProvider, string connectionString)
        {
            IDbConnection connection = null;

            switch (dbProvider)
            {
                case DBProvider.Oracle:
                    connection = new OracleConnection(connectionString);
                    break;
                case DBProvider.MsSqlServer:
                    connection = new SqlConnection(connectionString);
                    break;
                default:
                    break;
            }

            return connection;
        }

        #endregion
    }
}
