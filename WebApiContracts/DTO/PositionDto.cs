using System;
using System.Collections.Generic;
using System.Text;
using WebApiContracts.DTO.Base;

namespace WebApiContracts.DTO
{
	public class PositionDto : EntityDto<int>
	{
		public string Title { get; set; }

		public byte Grade { get; set; }

		public Error Error { get; set; }
	}
}
