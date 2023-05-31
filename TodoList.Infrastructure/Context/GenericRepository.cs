using TodoList.Domain.Common.Entities;
using TodoList.Domain.Common.Interfaces;

namespace TodoList.Infrastructure.Context;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
	public Task AddEntity(T entity)
	{
		throw new NotImplementedException();
	}

	public Task AddRangeEntities(List<T> entities)
	{
		throw new NotImplementedException();
	}

	public void DeleteEntity(T entity)
	{
		throw new NotImplementedException();
	}

	public Task DeleteEntity(long entityId)
	{
		throw new NotImplementedException();
	}

	public void DeletePermanent(T entity)
	{
		throw new NotImplementedException();
	}

	public Task DeletePermanent(long entityId)
	{
		throw new NotImplementedException();
	}

	public ValueTask DisposeAsync()
	{
		throw new NotImplementedException();
	}

	public void EditEntity(T entity)
	{
		throw new NotImplementedException();
	}

	public Task<List<T>> GetAllEntities()
	{
		throw new NotImplementedException();
	}

	public IQueryable<T> GetQuery()
	{
		throw new NotImplementedException();
	}

	public Task SaveChanges()
	{
		throw new NotImplementedException();
	}
}
