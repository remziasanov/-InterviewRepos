using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiContracts.DTO
{
	public class EmployeePositionDto
	{
		public int EmployeeId { get; set; }
		public EmployeeDto Employee { get; set; }
		public int PositionId { get; set; }
		public PositionDto Position { get; set; }
	}
}
