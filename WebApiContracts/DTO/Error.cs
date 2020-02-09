using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiContracts.DTO
{
	public class Error
	{
		public byte Code { get; set; }
		public string TextError { get; set; }
	}
}
