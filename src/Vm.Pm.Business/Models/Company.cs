using System.Collections.Generic;

namespace Vm.Pm.Business.Models
{
	public class Company : Entity
	{
		public string DocumentNumber { get; set; }
		public string FEIEIN { get; set; }
		public string LegalName { get; set; }
		public string TradeName { get; set; }
		public IEnumerable<Contact> Contacts { get; set; }
		public IEnumerable<Phone> Phones { get; set; }
		public IEnumerable<Address> Addresses { get; set; }
	}
}
