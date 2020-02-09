using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AppServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApiContracts.DTO;

namespace WebApiService.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class PositionController : ControllerBase
	{
		protected readonly IEmployeeService employeeService;
		protected readonly IPositionsService positionService;

		public PositionController(IEmployeeService employeeService, IPositionsService positionService)
		{
			this.employeeService = employeeService;
			this.positionService = positionService;
		}



		// GET api/values
		[HttpGet]
		public IList<PositionDto> Get()
		{
			return positionService.GetAll();
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public async Task<PositionDto> Get(int id)
		{
			return await positionService.GetAsync(id);
		}

		// POST api/values
		[HttpPost]
		public async Task<HttpResponseMessage> Post([FromBody] PositionDto position)
		{
			if (position != null)
			{
				PositionDto temp = new PositionDto();

				temp = position;

				PositionDto posdto = await positionService.CreateAsync(temp);

				if (posdto != null)
					return new HttpResponseMessage(HttpStatusCode.Created);

			}
			return new HttpResponseMessage(HttpStatusCode.BadRequest);
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public async Task<HttpResponseMessage> Put(int id, [FromBody] PositionDto value)
		{
			PositionDto posdto =  await positionService.GetAsync(id);
			if(posdto!= null)
			{
				posdto = await positionService.Update(value);
				if(posdto != null)
					return new HttpResponseMessage(HttpStatusCode.OK);
			return new HttpResponseMessage(HttpStatusCode.NotModified);
			}
			else
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public async Task<HttpResponseMessage> Delete(int id)
		{
			IList<EmployeeDto> emps = await employeeService.GetByPosition(id);

			if (emps == null)
			{
				bool isDeleted = await positionService.Delete(id);
				if (isDeleted)
					return new HttpResponseMessage(HttpStatusCode.OK);
				else
					return new HttpResponseMessage(HttpStatusCode.NotFound);

			}
			return new HttpResponseMessage(HttpStatusCode.BadRequest);
		}
	}
}