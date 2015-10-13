using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MolluskRecognition.DAL.DataModels;

namespace MolluskRecognition.DAL.Queries
{
    public interface IQuery<out T> : IDisposable where T : IEntity
    {
        IQueryable<T> Get();
    }

    public abstract class BaseQuery<T> : IQuery<T> where T : IEntity
    {
        protected MolluskRecognitionContext Context = new MolluskRecognitionContext();

        public abstract IQueryable<T> Get();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        ~BaseQuery()
        {
            Dispose(false);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }
        }
    }

    public sealed class FamilyQuery : BaseQuery<Family>
    {
        public override IQueryable<Family> Get()
        {
            return Context.Families.AsQueryable();
        }
    }
}
