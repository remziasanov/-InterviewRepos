using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
	public class EmployeePosition
	{
		public int EmployeeId { get; set; }
		public Employee Employee { get; set; }
		public int PositionId { get; set; }
		public Position Position { get; set; }

	}
}
