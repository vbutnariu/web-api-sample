using Microsoft.EntityFrameworkCore;
using Demo.Common.Exceptions.Data;
using Demo.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.Data.DbContext
{
    public class ApplicationDbTransaction : IDbTransaction
    {
        private readonly bool throwExceptionOnRollback;
        private bool disposedValue;

        public ApplicationDbTransaction(IDbContext context, bool throwExceptionOnRollbackForInnerTrasactions = true)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            Context = context;
            this.throwExceptionOnRollback = throwExceptionOnRollbackForInnerTrasactions;
        }
        public int TranCount => Context.TranCount;

        public IDbContext Context { get; }

        public void Commit()
        {
            try
            {
                if (Context.TranCount <= 1)
                {
                    (Context as Microsoft.EntityFrameworkCore.DbContext).Database.CurrentTransaction.Commit();
                }
            }
            finally
            {
                Context.TranCount--;
            }
        }

        public void Rollback()
        {
            Rollback(new RollbackException());
        }

        public void Rollback(Exception exceptionToThrow)
        {
            try
            {
                if (Context.TranCount <= 1)
                {
                    (Context as Microsoft.EntityFrameworkCore.DbContext).Database.CurrentTransaction.Rollback();
                }
                else
                {
                    if (throwExceptionOnRollback)
                    {
                        throw exceptionToThrow;
                    }
                    else
                    {
                        Context.TranCount = 1;
                        (Context as Microsoft.EntityFrameworkCore.DbContext).Database.CurrentTransaction.Rollback();
                    }
                }
            }
            finally
            {
                Context.TranCount--;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                    if (Context.TranCount < 1)
                    {
                        var ctx = Context as Microsoft.EntityFrameworkCore.DbContext;
                        ctx.Database.CurrentTransaction?.Dispose();
                    }
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
