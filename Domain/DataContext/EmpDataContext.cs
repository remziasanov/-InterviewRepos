using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DataContext
{
	public class EmpDataContext:DbContext
	{
		public EmpDataContext(DbContextOptions<EmpDataContext> dbContextOption) : base(dbContextOption)
		{

		}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Position> Positions { get; set; }
		public DbSet<EmployeePosition> EmployeePositions { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<EmployeePosition>()
				.HasKey(ep => new { ep.EmployeeId, ep.PositionId });

			modelBuilder.Entity<EmployeePosition>()
				.HasOne(ep => ep.Employee)
				.WithMany(b => b.EmployeePositions)
				.HasForeignKey(ep => ep.EmployeeId);

			modelBuilder.Entity<EmployeePosition>()
				.HasOne(p => p.Position)
				.WithMany(c => c.EmployeePositions)
				.HasForeignKey(p => p.PositionId);
		}
	}
}

