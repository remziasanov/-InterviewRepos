using Domain.Data.Repositories.Base;
using Domain.DataContext;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Repositories
{
	public class EmployeeRepository : BaseRepository<Employee, int, EmpDataContext>, IEmployeeRepository
	{
		public EmployeeRepository(EmpDataContext context) : base(context)
		{

		}

		public async override Task<Employee> Get(int id)
		{
			IQueryable<Employee> employees = _dbContext.Employees.Include(x => x.EmployeePositions).Where(x => x.Id == id);

			return await employees.SingleOrDefaultAsync();
		}

		public IQueryable<Employee> GetByPosition(int posId)
		{
			IQueryable<Employee> employees = _dbContext.Employees
											.Join(
											_dbContext.EmployeePositions.Where(x => x.PositionId == posId),
											empl => empl.Id,
											emplPos => emplPos.EmployeeId,
											(empl, emplPos) => empl)
											.Include(x => x.EmployeePositions);

			return employees;
		}
	}
	
}
