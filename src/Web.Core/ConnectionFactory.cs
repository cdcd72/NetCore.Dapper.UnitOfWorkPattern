using Microsoft.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Web.Core.Enum;

namespace Web.Core
{
    public class ConnectionFactory
    {
        #region Properties

        private readonly DBProvider _dBProvider;
        private readonly string _connectionString;

        #endregion

        #region Constructor

        public ConnectionFactory(DBProvider dBProvider, string connectionString)
        {
            _dBProvider = dBProvider;
            _connectionString = connectionString;
        }

        #endregion

        /// <summary>
        /// 建立資料庫連線
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            IDbConnection connection = null;

            switch (_dBProvider)
            {
                case DBProvider.Oracle:
                    connection = new OracleConnection(_connectionString);
                    break;
                case DBProvider.MsSqlServer:
                    connection = new SqlConnection(_connectionString);
                    break;
                default:
                    break;
            }

            return connection;
        }
    }
}
