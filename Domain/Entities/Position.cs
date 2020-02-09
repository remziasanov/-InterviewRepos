using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
	public class Position : EntityBase<int>
	{
		[Required]
		public string Title { get; set; }
		[Range(1, 15)]
		public byte Grade { get; set; }

		public virtual ICollection<EmployeePosition> EmployeePositions { get; set; }

	}
}
