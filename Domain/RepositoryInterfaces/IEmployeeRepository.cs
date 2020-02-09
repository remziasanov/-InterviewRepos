using Domain.Entities;
using Domain.RepositoryInterfaces.Base;
using System.Linq;

namespace Domain.RepositoryInterfaces
{
	public interface IEmployeeRepository: IRepositoryBase<Employee,int>
	{
		IQueryable<Employee> GetByPosition(int posId);
	}
}
