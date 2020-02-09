using Domain.Data.Repositories;
using Domain.DataContext;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AppServices.Interfaces;
using AppServices.Services;

namespace RegisterComponent
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection Register(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<EmpDataContext>(options => options.UseSqlServer(connectionString));
			services.AddTransient<IEmployeeRepository, EmployeeRepository>();
			services.AddTransient<IPositionRepository, PositionRepository>();
			services.AddTransient<IEmployeeService, EmployeeService>();
			services.AddTransient<IPositionsService, PositionService>();
			return services;
		}
	}
}
