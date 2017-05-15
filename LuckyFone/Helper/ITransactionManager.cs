using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace LuckyFone.Helper
{
    public interface ITransactionManager
    {
        ITransactionManager BeginTransaction();
        void Commit();
        void Dispose();
        void Finished();
        void Rollback();

        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        string ConnectionName { get; }
        string ConnectionString { get; }
        bool IsTransactionInstance { get; }
    }
}