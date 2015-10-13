using Mollusk.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mollusk.Db4oRepository
{
	public class Db4oRepository<T> : IRepository<T>
		where T : IEntity
	{
	}
}