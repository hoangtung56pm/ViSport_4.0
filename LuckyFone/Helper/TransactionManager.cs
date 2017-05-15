using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;

namespace LuckyFone.Helper
{

    public class TransactionManager : ITransactionManager
    {
        public static ITransactionManager Instance(string provider)
        {
            return new TransactionManager(provider,
                                          ConfigurationManager.ConnectionStrings[provider].ConnectionString,
                                          false);
        }

        public TransactionManager(String provider, String connectionString, bool isTransactionInstance)
        {
            this.ConnectionName = provider;
            this.ConnectionString = connectionString;
            this.IsTransactionInstance = isTransactionInstance;
        }
        public static ITransactionManager Instance()
        {
            return Instance("localsqlLkf_Vms");
        }
        public ITransactionManager BeginTransaction()
        {

            if (Connection == null) Connection = (new OracleTransactionFilter()).InstanceConnection();
            Connection.ConnectionString = ConnectionString;
            if (Connection.State != System.Data.ConnectionState.Open) Connection.Open();
            this.Transaction = Connection.BeginTransaction();
            IsTransactionInstance = true;
            return this;
        }
        public void Commit()
        {
            if (this.Transaction != null)
            {
                Transaction.Commit();
            }
        }
        public void Rollback()
        {
            if (this.Transaction != null)
            {
                Transaction.Rollback();
            }
        }
        public void Finished()
        {
            if (Transaction != null) { Transaction.Dispose(); }
            Transaction = null;
        }
        public void Dispose()
        {
            if (Connection != null && Connection.State != System.Data.ConnectionState.Closed)
            {
                Connection.Close();
                Connection.Dispose();
            }
            if (Transaction != null) { Transaction.Dispose(); }
            Transaction = null;
            Connection = null;
        }

        public IDbConnection Connection { get; private set; }
        public IDbTransaction Transaction { get; private set; }
        public string ConnectionString { get; private set; }
        public string ConnectionName { get; private set; }
        public bool IsTransactionInstance { get; private set; }
    }

}