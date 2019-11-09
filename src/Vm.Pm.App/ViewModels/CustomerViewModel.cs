using System;
using System.Collections.Generic;

namespace Vm.Pm.App.ViewModels
{
	public class CustomerViewModel
	{
		public Guid Id { get; set; }
		public Guid CompanyId { get; set; }
		public string Document { get; set; }
		public string Description { get; set; }
		public string Email { get; set; }
		public IEnumerable<ContactViewModel> Contacts { get; set; }
		public IEnumerable<PhoneViewModel> Phones { get; set; }
		public IEnumerable<AddressViewModel> Addresses { get; set; }
		public IEnumerable<CollaboratorCustomerViewModel> CollaboratorsCustomers { get; set; }
		public CompanyViewModel Company { get; set; }
		public IEnumerable<CompanyViewModel> Companies { get; set; }
	}
}
