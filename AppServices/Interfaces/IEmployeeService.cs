using AppServices.Interfaces.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiContracts.DTO;

namespace AppServices.Interfaces
{
	public interface IEmployeeService : IBaseService<EmployeeDto, int>
	{
		Task<IList<EmployeeDto>> GetByPosition(int posId);
	}
}
