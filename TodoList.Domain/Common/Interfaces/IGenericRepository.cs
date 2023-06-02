using TodoList.Domain.Common.Entities;

namespace TodoList.Domain.Common.Interfaces;

public interface IGenericRepository<T> : IAsyncDisposable where T : BaseEntity
{
	Task AddEntity(T entity);
	Task AddRangeEntities(List<T> entities);
	Task<T> GetEntityById(long entityId);
	Task<List<T>> GetAllEntities();
	IQueryable<T> GetQuery();
	void EditEntity(T entity);
	void DeleteEntity(T entity);
	Task DeleteEntity(long entityId);
	Task SaveChanges();
}
