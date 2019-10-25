using System;
using System.Collections.Generic;

namespace Vm.Pm.Business.Models
{
	public class Collaborator : Entity
	{
		public Guid CompanyId { get; set; }
		public string Name { get; set; }
		public IEnumerable<Contact> Contacts { get; set; }
		public IEnumerable<Phone> Phones { get; set; }
		public IEnumerable<Address> Addresses { get; set; }
		public Company Company { get; set; }
	}
}
