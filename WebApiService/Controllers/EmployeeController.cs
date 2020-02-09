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
	public class EmployeeController : ControllerBase
	{
		protected readonly IEmployeeService employeeService;
		protected readonly IPositionsService positionService;

		public EmployeeController(IEmployeeService employeeService, IPositionsService positionService)
		{
			this.employeeService = employeeService;
			this.positionService = positionService;
		}



		// GET api/values
		[HttpGet]
		public ActionResult<string> Get()
		{
			return  "Server web api started!";
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public async Task<EmployeeDto> Get(int id)
		{
			return await employeeService.GetAsync(id);
		}

		[HttpGet("position/{id}")]
		public async Task<IList<EmployeeDto>> GetByPosition(int id)
		{
			return await employeeService.GetByPosition(id);

		}

		// POST api/values
		[HttpPost]
		public async Task<HttpResponseMessage> Post([FromBody] EmployeeDto employee)
		{

			if (employee != null)
			{
				EmployeeDto temp = new EmployeeDto();

				temp = employee;

				temp.Positions = positionService.GetAll(employee.Positions.Select(x => x.Id).ToArray()).ToArray();
				if(temp.Positions == null)
				{
					return new HttpResponseMessage(HttpStatusCode.NotFound);
				}

				EmployeeDto empdto = await employeeService.CreateAsync(temp);

				if (empdto != null && string.IsNullOrEmpty(empdto.Error.TextError))
					return new HttpResponseMessage(HttpStatusCode.Created);		

			}
			return new HttpResponseMessage(HttpStatusCode.BadRequest);
			
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public async Task<HttpResponseMessage> Put(int id, [FromBody] EmployeeDto value)
		{
			EmployeeDto empdto = await employeeService.GetAsync(id);
			if (empdto != null)
			{
				empdto = await employeeService.Update(value);
				if (empdto != null)
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
			EmployeeDto empdto = await employeeService.GetAsync(id);
			if (empdto != null)
			{
				bool isDeleted = await employeeService.Delete(id);
				if (isDeleted)
					return new HttpResponseMessage(HttpStatusCode.OK);
				else
					return new HttpResponseMessage(HttpStatusCode.NotFound);

			}
			return new HttpResponseMessage(HttpStatusCode.NotFound);
		}
	}
}
