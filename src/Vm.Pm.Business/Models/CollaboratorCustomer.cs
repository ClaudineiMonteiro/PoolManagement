using System;

namespace Vm.Pm.Business.Models
{
	public class CollaboratorCustomer
	{
		public Guid CollaboratorId { get; set; }
		public Collaborator Collaborator { get; set; }
		public Guid CustomerId { get; set; }
		public Customer Customer { get; set; }
	}
}
