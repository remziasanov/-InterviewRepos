using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataContext
{
	class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EmpDataContext>
	{
		public EmpDataContext CreateDbContext(string[] args)
		{
			var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Desktop\LogroconTestProject\Domain\DataContext\Database.mdf;Integrated Security=True";
			var builder = new DbContextOptionsBuilder<EmpDataContext>().UseSqlServer(connectionString);
			return new EmpDataContext(builder.Options);
		}
	}
}
