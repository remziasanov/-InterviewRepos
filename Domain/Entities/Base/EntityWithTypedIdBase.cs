using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Base
{
	public class EntityWithTypedIdBase<TId>
	{
		/// <summary>
		/// Identifier primary key
		/// </summary>
		public virtual TId Id { get; protected set; }
	}
}
