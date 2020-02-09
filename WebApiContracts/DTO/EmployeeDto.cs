using System;
using System.Collections.Generic;
using System.Text;
using WebApiContracts.DTO.Base;

namespace WebApiContracts.DTO
{
	public class EmployeeDto : EntityDto<int>
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Fathername { get; set; }

		public PositionDto[] Positions { get; set; }

		//public virtual ICollection<EmployeePositionDto> EmployeePositions { get; set; }

		public Error Error { get; set; }
	}
}
