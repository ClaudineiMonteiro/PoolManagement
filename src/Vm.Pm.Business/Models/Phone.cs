using System;
using Vm.Pm.Business.Enumerators;

namespace Vm.Pm.Business.Models
{
	public class Phone : Entity
	{
		public Guid? CompanyId { get; set; }
		public Guid? ContactId { get; set; }
		public Guid? CollaboratorId { get; set; }
		public Guid? CustomerId { get; set; }
		public string Number { get; set; }
		public TypePhone TypePhone { get; set; }
		public Company Company { get; set; }
		public Contact Contact { get; set; }
		public Collaborator Collaborator { get; set; }
		public Customer Customer { get; set; }
	}
}
