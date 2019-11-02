using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Interfaces
{
	public interface ICollaboratorRepository : IRepository<Collaborator>
	{
		Task<IEnumerable<Collaborator>> GetCollaboratorsByCompany(Guid companyId);
		Task<Collaborator> GetCollaboratorPhones(Guid id);
		Task<Collaborator> GetCollaboratorAddresses(Guid id);
		Task<Collaborator> GetCollaboratorContacts(Guid id);
		Task<Collaborator> GetCollaboratorPhonesAddressesContacts(Guid id);
	}
}
