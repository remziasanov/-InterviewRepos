using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
	public class Employee : EntityBase<int>
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
		public string Fathername { get; set; }

		public virtual ICollection<EmployeePosition> EmployeePositions { get; set; }
	}
}
