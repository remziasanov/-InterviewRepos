using Domain.Data.Repositories.Base;
using Domain.DataContext;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data.Repositories
{
	public class PositionRepository : BaseRepository<Position, int, EmpDataContext>, IPositionRepository
	{
		public PositionRepository(EmpDataContext context) : base(context)
		{

		}

		public IQueryable<Position> GetAll(int[] ids)
		{
			var result = _dbContext.Positions.Where(x => ids.Contains(x.Id));
			return result;
		}

		public async override Task<Position> Get(int id)
		{
			IQueryable<Position> employees = _dbContext.Positions.Include(x => x.EmployeePositions).Where(x => x.Id == id);
			return await employees.SingleOrDefaultAsync();
		}
	}
}
