using System;
using System.Collections;
using System.Collections.Generic;

namespace Vm.Pm.Business.Models
{
	public class Contact : Entity
	{
		public Guid? CompanyId { get; set; }
		public Guid? CollaboratorId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public IEnumerable<Phone> Phones { get; set; }
		public IEnumerable<Address> Adresses { get; set; }
		public Company Company { get; set; }
		public Collaborator Collaborator { get; set; }
	}
}
