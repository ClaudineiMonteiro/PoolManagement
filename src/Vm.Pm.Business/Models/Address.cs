using System;
using Vm.Pm.Business.Enumerators;

namespace Vm.Pm.Business.Models
{
	public class Address : Entity
	{
		public Guid CompanyId { get; set; }
		public Guid ContactId { get; set; }
		public Guid CollaboratorId { get; set; }
		public string PublicPlace { get; set; }
		public string Apt_Suite_Unit { get; set; }
		public string City { get; set; }
		public string State_Province { get; set; }
		public string ZipPostalCode { get; set; }
		public TypeAddress TypeAddress { get; set; }
		public Company Company { get; set; }
		public Contact Contact { get; set; }
		public Collaborator Collaborator { get; set; }
	}
}
