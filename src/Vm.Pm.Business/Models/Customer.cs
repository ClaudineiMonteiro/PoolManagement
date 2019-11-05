using System;
using System.Collections.Generic;

namespace Vm.Pm.Business.Models
{
	public class Customer : Entity
	{
		public Guid CompanyId { get; set; }
		public string Document { get; set; }
		public string Description { get; set; }
		public string Email { get; set; }
		public IEnumerable<Contact> Contacts { get; set; }
		public IEnumerable<Phone> Phones { get; set; }
		public IEnumerable<Address> Addresses { get; set; }
		public IEnumerable<CollaboratorCustomer> CollaboratorCustomers { get; set; }
		public Company Company { get; set; }
	}
}
