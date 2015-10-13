using System;

namespace Mollusk.Repository
{
	public interface IEntity
	{
		Guid Id { get; }
	}
}