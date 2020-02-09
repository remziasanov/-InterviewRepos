using Domain.Entities;
using Domain.RepositoryInterfaces.Base;
using System.Linq;

namespace Domain.RepositoryInterfaces
{
	public interface IPositionRepository: IRepositoryBase<Position,int>
	{
		IQueryable<Position> GetAll(int[] ids);
	}
}
