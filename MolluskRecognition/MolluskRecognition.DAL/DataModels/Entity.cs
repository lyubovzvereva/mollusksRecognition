using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolluskRecognition.DAL.DataModels
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }

    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        protected Entity(Guid id)
        {
            Id = id;
        }

        [Key] public Guid Id { get; set; }
    }
}
