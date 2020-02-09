using Domain.Entities.Base;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces.Base
{
	public interface IRepositoryBase<TEntity, TId> where TEntity : EntityWithTypedIdBase<TId>
	{
		IQueryable<TEntity> GetAll();
		Task<TEntity> Create(TEntity entity);
		Task<TEntity> Update(TEntity entity);
		Task<TEntity> Get(TId id);
		Task<TEntity> Delete(TId id);
		IQueryable<TEntity> GetAllWhere(Expression<Func<TEntity, bool>> predicate);
	}
}
