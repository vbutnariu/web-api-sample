using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.Data
{
    public interface IDbTransaction: IDisposable
    {
        void Commit();
        void Rollback();
        void Rollback(Exception exceptionToThrow);
        public int TranCount { get; }
    }
}
