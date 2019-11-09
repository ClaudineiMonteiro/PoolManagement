using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vm.Pm.App.ViewModels
{
	public class CollaboratorCustomerViewModel
	{
		public Guid CollaboratorId { get; set; }
		public CollaboratorViewModel Collaborator { get; set; }
		public Guid CustomerId { get; set; }
		public CustomerViewModel Customer { get; set; }
	}
}
