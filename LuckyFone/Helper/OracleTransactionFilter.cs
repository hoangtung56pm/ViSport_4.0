using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace LuckyFone.Helper
{

    public class OracleTransactionFilter
    {
        #region oracle client
        public IDbConnection InstanceConnection()
        {
            return OracleHelper.InstanceConnection();
        }
        #endregion
        #region Get Ojects
        public static IList<T> GetDataLits<T>(ITransactionManager transManager, string spName, params object[] parameterValue)
        {
            DataTable tbl = GetDataTable(transManager, spName, parameterValue);
            IList<T> obj = null;
            obj = ObjectHelper.FillTable<T>(tbl);
            return obj;
        }
        public static DataTable GetDataTable(ITransactionManager transManager, string spName, params object[] parameterValue)
        {
            DataSet set = ExecuteDataset(transManager, spName, parameterValue);
            if (set != null && set.Tables.Count > 0)
            {
                return set.Tables[0];
            }
            return null;
        }

        #endregion
        #region Origin function
        public static int ExecuteNonQuery(ITransactionManager transManager, string spName, params object[] parameterValue)
        {
            if (transManager.IsTransactionInstance)
            {
                return OracleHelper.ExecuteNonQuery(transManager.Transaction, spName, parameterValue);
            }
            else
            {
                return OracleHelper.ExecuteNonQuery(transManager.ConnectionString, spName, parameterValue);
            }
        }
        public static int ExecuteNonQuery(ITransactionManager transManager, string spName, ref object[] outParam, params object[] parameterValue)
        {
            if (transManager.IsTransactionInstance)
            {
                return OracleHelper.ExecuteNonQuery(transManager.Transaction, spName, ref outParam, parameterValue);
            }
            else
            {
                return OracleHelper.ExecuteNonQuery(transManager.ConnectionString, spName, ref outParam, parameterValue);
            }
        }
        public static IDataReader ExecuteReader(ITransactionManager transManager, string spName, params object[] parameterValue)
        {
            if (transManager.IsTransactionInstance)
            {
                return OracleHelper.ExecuteReader(transManager.Transaction, spName, parameterValue);
            }
            else
            {
                return OracleHelper.ExecuteReader(transManager.ConnectionString, spName, parameterValue);
            }
        }
        public static object ExecuteScalar(ITransactionManager transManager, string spName, params object[] parameterValue)
        {
            if (transManager.IsTransactionInstance)
            {
                return OracleHelper.ExecuteScalar(transManager.Transaction, spName, parameterValue);
            }
            else
            {
                return OracleHelper.ExecuteScalar(transManager.ConnectionString, spName, parameterValue);
            }
        }
        public static object ExecuteScalar(ITransactionManager transManager, string spName, ref object[] outParam, params object[] parameterValue)
        {
            if (transManager.IsTransactionInstance)
            {
                return OracleHelper.ExecuteScalar(transManager.Transaction, spName, ref outParam, parameterValue);
            }
            else
            {
                return OracleHelper.ExecuteScalar(transManager.ConnectionString, spName, ref outParam, parameterValue);
            }
        }
        public static DataSet ExecuteDataset(ITransactionManager transManager, string spName, params object[] parameterValue)
        {
            if (transManager.IsTransactionInstance)
            {
                return OracleHelper.ExecuteDataset(transManager.Transaction, spName, parameterValue);
            }
            else
            {
                return OracleHelper.ExecuteDataset(transManager.ConnectionString, spName, parameterValue);
            }
        }
        public static DataSet ExecuteDataset(ITransactionManager transManager, string spName, ref object[] outParam, params object[] parameterValue)
        {
            if (transManager.IsTransactionInstance)
            {
                return OracleHelper.ExecuteDataset(transManager.Transaction, spName, ref outParam, parameterValue);
            }
            else
            {
                return OracleHelper.ExecuteDataset(transManager.ConnectionString, spName, ref outParam, parameterValue);
            }
        }
        public static DataSet ExecuteDataset(string ConnectionString, string query)
        {

            return OracleHelper.ExecuteDataset(ConnectionString, CommandType.Text, query);

        }
        #endregion
    }

}