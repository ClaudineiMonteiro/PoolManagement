using System;

namespace Vm.Pm.Business.Models
{
	public abstract class Entity
	{
		public Guid Id { get; set; }
		public DateTime RegistrationDate { get; set; }
		public DateTime? LastUpdatedDate { get; set; }
		public bool Active { get; set; }

		protected Entity()
		{
			Id = Guid.NewGuid();
		}
	}
}
