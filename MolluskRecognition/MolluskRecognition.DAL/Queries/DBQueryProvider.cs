using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MolluskRecognition.DAL.DataModels;

namespace MolluskRecognition.DAL.Queries
{
    public interface IDBQueryProvider
    {
        IQueryable<T> GetBaseQuery<T>() where T : Entity;
    }

    public class DBQueryProvider : IDBQueryProvider, IDisposable
    {
        protected IMolluskRecognitionContext Context;
        protected IQueriesList Queries;

        [ImportingConstructor]
        public DBQueryProvider(IMolluskRecognitionContext context, IQueriesList queries)
        {
            Context = context;
            Queries = queries;
        }

        [NotNull]
        public IQueryable<T> GetBaseQuery<T>() where T : Entity
        {
            var result = Queries.Queries.FirstOrDefault(q => q.Type == typeof(T).Name + "Base") as QueryWrapper<T>;

            if (result == null)
                throw new ArgumentException($"Query for {typeof(T).Name} not found");

            return result.Query(Context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DBQueryProvider()
        {
            Dispose(false);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || Context == null) return;

            Context.Dispose();
            Context = null;
        }
    }
}
