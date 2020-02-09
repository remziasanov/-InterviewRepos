using System;
using System.Collections.Generic;
using System.Text;

namespace WebApiContracts.DTO.Base
{
	public class EntityWithTypedIdBaseDto<TId>
	{
		public virtual TId Id { get; set; }
	}
}
