#region Namespace Declaration
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
#endregion

/// <summary>
/// Core.Logging namespace that contains methods to log information on the selected log target.
/// </summary>

namespace SUPMS.Infrastructure.AsyncLogger
{
    /// <summary>
    /// Factory class to instantiate data provider classes
    /// </summary>
    internal static class DBFactory
    {
        #region Methods

        #region GetConnection
        /// <summary>
        /// Gets the database connection for required provider
        /// </summary>
        /// <param name="providerType">Holds the providerType Value, can be SQL,ORACLE,OLEDB or Odbc </param>
        /// <returns>Returns the database connection object</returns>
        public static IDbConnection GetConnection(DataProvider providerType)
        {
            IDbConnection iDbConnection = null;
            switch (providerType)
            {
                case DataProvider.SqlServer:
                    iDbConnection = new SqlConnection();
                    break;
                case DataProvider.OleDb:
                    iDbConnection = new OleDbConnection();
                    break;
                case DataProvider.Odbc:
                    iDbConnection = new OdbcConnection();
                    break;
                case DataProvider.Oracle:
                    //iDbConnection = new OracleConnection();
                    break;
                default:
                    return null;
            }

            return iDbConnection;
        }
        #endregion
        //
        #region GetCommand
        /// <summary>
        /// Gets the database command object for required provider
        /// </summary>
        /// <param name="providerType">Holds the providerType Value, can be SQL,ORACLE,OLEDB or Odbc</param>
        /// <returns>Returns the databae command object</returns>
        public static IDbCommand GetCommand(DataProvider providerType)
        {
            switch (providerType)
            {
                case DataProvider.SqlServer:
                    return new SqlCommand();
                case DataProvider.OleDb:
                    return new OleDbCommand();
                case DataProvider.Odbc:
                    return new OdbcCommand();
                case DataProvider.Oracle:
                //return new OracleCommand();
                default:
                    return null;
            }

        }

        #endregion
        //
        #region GetDataAdapter
        /// <summary>
        /// Gets the DataAdapter object for required provider
        /// </summary>
        /// <param name="providerType">Holds the providerType Value, can be SQL,ORACLE,OLEDB or Odbc</param>
        /// <returns>Returns the DataAdapter object</returns>
        public static IDbDataAdapter GetDataAdapter(DataProvider providerType)
        {
            switch (providerType)
            {
                case DataProvider.SqlServer:
                    return new SqlDataAdapter();
                case DataProvider.OleDb:
                    return new OleDbDataAdapter();
                case DataProvider.Odbc:
                    return new OdbcDataAdapter();
                case DataProvider.Oracle:
                //return new OracleDataAdapter();
                default:
                    return null;
            }
        }
        #endregion
        //
        #region GetTransaction
        /// <summary>
        /// Gets the transaction object for required provider
        /// </summary>
        /// <param name="providerType">Holds the providerType Value, can be SQL,ORACLE,OLEDB or Odbc</param>
        /// <returns>Returns the database transaction object</returns>
        public static IDbTransaction GetTransaction(DataProvider providerType)
        {
            IDbConnection iDbConnection = GetConnection(providerType);
            IDbTransaction iDbTransaction = iDbConnection.BeginTransaction();
            return iDbTransaction;
        }
        #endregion
        //
        #region GetParameter
        /// <summary>
        /// Gets the database parameter for required provider
        /// </summary>
        /// <param name="providerType">Holds the providerType Value, can be SQL,ORACLE,OLEDB or Odbc</param>
        /// <returns>Returns the database pararameter object</returns>
        public static IDataParameter GetParameter(DataProvider providerType)
        {
            IDataParameter iDataParameter = null;
            switch (providerType)
            {
                case DataProvider.SqlServer:
                    iDataParameter = new SqlParameter();
                    break;
                case DataProvider.OleDb:
                    iDataParameter = new OleDbParameter();
                    break;
                case DataProvider.Odbc:
                    iDataParameter = new OdbcParameter();
                    break;
                case DataProvider.Oracle:
                    //iDataParameter = newOracleParameter();
                    break;

            }
            return iDataParameter;
        }
        #endregion
        //
        #region GetParameters
        /// <summary>
        /// Gets the Array of parameters for required provider
        /// </summary>
        /// <param name="providerType">Holds the providerType Value, can be SQL,ORACLE,OLEDB or Odbc</param>
        /// <param name="paramsCount">Returns the array of Db parameters</param>
        /// <returns></returns>
        public static IDbDataParameter[] GetParameters(DataProvider providerType, int paramsCount)
        {
            IDbDataParameter[] idbParams = new IDbDataParameter[paramsCount];

            switch (providerType)
            {
                case DataProvider.SqlServer:
                    for (int i = 0; i < paramsCount; ++i)
                    {
                        idbParams[i] = new SqlParameter();
                    }
                    break;
                case DataProvider.OleDb:
                    for (int i = 0; i < paramsCount; ++i)
                    {
                        idbParams[i] = new OleDbParameter();
                    }
                    break;
                case DataProvider.Odbc:
                    for (int i = 0; i < paramsCount; ++i)
                    {
                        idbParams[i] = new OdbcParameter();
                    }
                    break;
                case DataProvider.Oracle:
                    //for (int i = 0; i < int ParamsLength; ++i)
                    //{
                    //    idbParams[i] = new OracleParameter();
                    //}
                    break;
                default:
                    idbParams = null;
                    break;
            }
            return idbParams;
        }
        #endregion

        #endregion
    }

    internal sealed class DataHelper
    {
        /// <summary>
        /// Private Constructor
        /// </summary>
        private DataHelper() { }

        /// <summary>
        /// DBProvider as DataProvider
        /// </summary>
        public static DataProvider DBProvider
        {
            get;
            set;
        }

        /// <summary>
        /// AttachParameters method
        /// </summary>
        /// <param name="command">command as IDbCommand</param>
        /// <param name="commandParameters">commandParameters as IDbDataParameter[]</param>
        private static void AttachParameters(IDbCommand command, IDbDataParameter[] commandParameters)
        {
            foreach (IDbDataParameter p in commandParameters)
            {
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }

                command.Parameters.Add(p);
            }
        }

        /// <summary>
        /// AssignParameterValues method
        /// </summary>
        /// <param name="commandParameters">commandParameters as IDbDataParameter[]</param>
        /// <param name="parameterValues">parameterValues as object[]</param>
        private static void AssignParameterValues(IDbDataParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                return;
            }

            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            for (int i = 0, j = commandParameters.Length; i < j; i++)
            {
                commandParameters[i].Value = parameterValues[i];
            }
        }

        /// <summary>
        /// PrepareCommand method
        /// </summary>
        /// <param name="command">command as IDbCommand</param>
        /// <param name="connection">connection as IDbConnection</param>
        /// <param name="transaction">transaction as IDbTransaction</param>
        /// <param name="commandType">commandType as CommandType</param>
        /// <param name="commandText">commandText as text</param>
        /// <param name="commandParameters">commandParameters as IDbDataParameter[]</param>
        private static void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDbDataParameter[] commandParameters)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            command.Connection = connection;

            command.CommandText = commandText;

            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            command.CommandType = commandType;

            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }

            return;
        }

        /// <summary>
        /// ExecuteNonQuery method
        /// </summary>
        /// <param name="connectionString">connectionString as string</param>
        /// <param name="commandType">commandType as CommandType</param>
        /// <param name="commandText">commandText as string</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(connectionString, commandType, commandText, (IDbDataParameter[])null);
        }


        /// <summary>
        /// ExecuteNonQuery method
        /// </summary>
        /// <param name="connectionString">connectionString as string</param>
        /// <param name="commandType">commandType as CommandType</param>
        /// <param name="commandText">commandText as string</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            using (IDbConnection cn = DBFactory.GetConnection(DBProvider))
            {
                cn.ConnectionString = connectionString;
                cn.Open();
                return ExecuteNonQuery(cn, commandType, commandText, commandParameters);
            }
        }

        /// <summary>
        /// ExecuteNonQuery method
        /// </summary>
        /// <param name="connectionString">connection as IDbConnection</param>
        /// <param name="commandType">commandType as CommandType</param>
        /// <param name="commandText">commandText as string</param>
        /// /// <param name="commandText">commandParameters as IDbDataParameter[]</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(IDbConnection connection, CommandType commandType, string commandText, params IDbDataParameter[] commandParameters)
        {
            IDbCommand cmd = DBFactory.GetCommand(DBProvider);
            PrepareCommand(cmd, connection, (IDbTransaction)null, commandType, commandText, commandParameters);
            int retval = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return retval;
        }

    }
}