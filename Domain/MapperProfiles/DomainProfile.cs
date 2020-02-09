using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApiContracts.DTO;

namespace Domain.MapperProfiles
{
	public class DomainProfile : Profile
	{
		public DomainProfile()
		{
			CreateMap<Employee, EmployeeDto>()
				.ForMember(dest => dest.Positions, opt => opt.MapFrom(src => src.EmployeePositions.Select(x=>x.Position).ToArray()));
			CreateMap<EmployeeDto,Employee>();
			CreateMap<Position, PositionDto>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title));

			CreateMap<PositionDto, Position>();
		}
	}
}
