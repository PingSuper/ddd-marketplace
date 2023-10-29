using System;
namespace Marketplace.Framework
{
	public interface IEntityStore
	{
		/// <summary>
		/// Loads an entity by id
		/// </summary>
		Task<T> Load<T>(string entityId) where T : Entity;

		/// <summary>
		/// Persists an entity
		/// </summary>
		Task Save<T>(T entity) where T : Entity;

		Task<bool> Exists<T>(string entityId);
	}
}

