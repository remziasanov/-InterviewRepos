using AppServices.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiContracts.DTO;

namespace AppServices.Services
{
	public class PositionService : IPositionsService
	{
		protected readonly IPositionRepository positionRepository;
		protected readonly IMapper mapper;

		public PositionService(IPositionRepository positionRepository, IMapper mapper)
		{
			this.positionRepository = positionRepository;
			this.mapper = mapper;
		}
		public async Task<PositionDto> CreateAsync(PositionDto entity)
		{
			if(entity != null)
			{
				if (entity.Grade >= 1 && entity.Grade <= 15)
				{
					if (!string.IsNullOrEmpty(entity.Title))
					{
						Position position = mapper.Map<Position>(entity);
						position = await positionRepository.Create(position);
						return mapper.Map<PositionDto>(position);
					}
				}
			}
			return null;
		}

		public async Task<bool> Delete(int id)
		{
			Position pos = await positionRepository.Get(id);
			if (pos != null)
			{
				if (await positionRepository.Delete(id) != null)
					return true;
			}
			return false;

		}

		public async Task<PositionDto> GetAsync(int id)
		{
			Position position = await positionRepository.Get(id);
			if (position != null)
			{
			    PositionDto positionDto = mapper.Map<PositionDto>(position);
				return positionDto;
					
			}
			else
			{
				return null;
			}
		}

		public IList<PositionDto> GetAll()
		{
			IList<Position> res = positionRepository.GetAll().ToList();
			IList <PositionDto> positionDtos = mapper.Map<IList<PositionDto>>(res);
			return positionDtos;
		}

		public async Task<PositionDto> Update(PositionDto entity)
		{
			var result = mapper.Map<Position>(entity);
			await positionRepository.Update(result);
			return entity;
		}

		public IList<PositionDto> GetAll(int[] ids)
		{
			if(ids.Count() >0)
			{
				IList<Position> positions = positionRepository.GetAll(ids).ToList();
				if (positions != null)
				{
					IList<PositionDto> positionDtos = mapper.Map<IList<PositionDto>>(positions);
					return positionDtos;
				}
			}
			return null;
		}
	}
}
