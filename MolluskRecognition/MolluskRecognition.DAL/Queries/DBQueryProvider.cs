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
    /// <summary>
    /// Interface for all DB readonly queries access. For write-only access see Commands
    /// </summary>
    public interface IDBQueryProvider
    {
        IQueryable<T> GetBaseQuery<T>() where T : Entity;
    }

    [Export(typeof(IDBQueryProvider))]
    public class DBQueryProvider : IDBQueryProvider, IDisposable
    {
        protected MolluskRecognitionContext Context;
        protected IQueriesList Queries;

        public DBQueryProvider(MolluskRecognitionContext context, IQueriesList queries)
        {
            Queries = queries;
            Context = context;
        }

        [ImportingConstructor]
        public DBQueryProvider(IQueriesList queries)
        {
            Queries = queries;
            Context = new MolluskRecognitionContext();
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
