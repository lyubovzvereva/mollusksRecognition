using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MolluskRecognition.DAL.DataModels;

namespace MolluskRecognition.DAL.Queries
{
    public interface IQueryWrapper
    {
        string Type { get; }
    }

    public class QueryWrapper<T> : IQueryWrapper
        where T : Entity
    {
        public string Type { get; }
        public Func<IMolluskRecognitionContext, IQueryable<T>> Query { get; }

        public QueryWrapper(string type, Func<IMolluskRecognitionContext, IQueryable<T>> query)
        {
            Type = type;
            Query = query;
        }
    }
}
