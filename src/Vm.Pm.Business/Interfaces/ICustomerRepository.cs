using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Interfaces
{
	public interface ICustomerRepository : IRepository<Customer>
	{
		Task<IEnumerable<Customer>> GetCustomersByCompany(Guid companyId);
		Task<IEnumerable<Customer>> GetCustomersByCollaborator(Guid collaboratorId);
		Task<Customer> GetCustomerPhones(Guid id);
		Task<Customer> GetCustomerAddresses(Guid id);
		Task<Customer> GetCustomerContacts(Guid id);
		Task<Customer> GetCustomerCollaborators(Guid id);
		Task<Customer> GetCustomerPhonesAddressesContactsCollaborators(Guid id);
	}
}
