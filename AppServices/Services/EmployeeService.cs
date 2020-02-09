using AppServices.Interfaces;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using AutoMapper;
using System.Threading.Tasks;
using WebApiContracts.DTO;
using Domain.Entities;
using System.Linq;

namespace AppServices.Services
{
	
	public class EmployeeService : IEmployeeService
	{
		protected readonly IEmployeeRepository employeeRepository;
		protected readonly IPositionRepository positionRepository;
		protected readonly IMapper mapper;
		public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IPositionRepository positionRepository)
		{
			this.employeeRepository = employeeRepository;
			this.positionRepository = positionRepository;
			this.mapper = mapper;
		}
		public async Task<EmployeeDto> CreateAsync(EmployeeDto entity)
		{

			if (entity != null)
			{
				if(string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Surname) || entity.Positions.Length <=0)
				{
					return GetError("One of the required fields is not filled",4);
				}

				Employee result = null;
				try
				{
					result = mapper.Map<Employee>(entity);

					List<Position> positions = mapper.Map<List<Position>>(entity.Positions.ToList());

					if(positions != null)
					{
						ICollection<EmployeePosition> tempPositions = new List<EmployeePosition>();

						foreach (var item in positions)
						{
							EmployeePosition tempPos = new EmployeePosition();
							tempPos.Position = item;
							tempPos.PositionId = item.Id;
							tempPos.Employee = result;
							tempPositions.Add(tempPos);
						}

						result.EmployeePositions = tempPositions;
					}

				}
				catch (Exception ex)
				{
					return GetError(ex.Message.ToString(), 1);
				}

				Employee employee = await employeeRepository.Create(result);
				if (employee != null)
				{
					entity.Id = employee.Id;
					return entity;
				}
				return GetError("Employee error create", 4);
			}
			else
			{
				return GetError("Employee is null ", 20);
			}
		}
		private EmployeeDto GetError(string error, byte code)
		{
			Error newError = new Error()
			{
				TextError = error,
				Code = code

			};
			return new EmployeeDto()
			{
				Error = newError

			};
		}

		public async Task<bool> Delete(int id)
		{
			Employee emp= await employeeRepository.Get(id);
			if (emp != null)
			{
				if( await employeeRepository.Delete(id) != null )
				return true;
			}
			return false;
		}

		public async Task<EmployeeDto> GetAsync(int id)
		{
			Employee employee = await employeeRepository.Get(id);
			if (employee != null)
			{
				List<PositionDto> positions = new List<PositionDto>();

				foreach (var pos in employee.EmployeePositions.Select(x=>x.PositionId))
				{
					Position temp = await positionRepository.Get(pos);
					PositionDto positionDto = mapper.Map<PositionDto>(temp);
					positions.Add(positionDto);
				}
				EmployeeDto employeeDto = mapper.Map<EmployeeDto>(employee);
				employeeDto.Positions = positions.ToArray();
				return employeeDto;
			}
			else
			{
				return GetError(string.Format("This employee {0} was not found",id),5);
			}
		}

		public IList<EmployeeDto> GetAll()
		{
			throw new NotImplementedException();
		}

		public async Task<EmployeeDto> Update(EmployeeDto entity)
		{
			var result = mapper.Map<Employee>(entity);

			Employee emp = await employeeRepository.Update(result);
			if(emp !=null)
			return entity;
			return GetError(string.Format("this employee {0} is not updated", entity.Id),34);
		}

		public async Task<IList<EmployeeDto>> GetByPosition(int posId)
		{
			List<Employee> employees = employeeRepository.GetByPosition(posId).ToList();
			List<EmployeeDto> employeeDtos = new List<EmployeeDto>();
			if (employees.Any())
			{
				foreach (var employee in employees)
				{
					EmployeeDto empDtotemp = null;
					List<PositionDto> positions = new List<PositionDto>();

					foreach (var pos in employee.EmployeePositions.Select(x => x.PositionId))
					{
						Position temp = await positionRepository.Get(pos);
						PositionDto positionDto = mapper.Map<PositionDto>(temp);
						positions.Add(positionDto);
					}
					empDtotemp = mapper.Map<EmployeeDto>(employee);
					empDtotemp.Positions = positions.ToArray();
					employeeDtos.Add(empDtotemp);
					positions = null;
				}
				return employeeDtos;
			}
			return null;
		}
	}
}
