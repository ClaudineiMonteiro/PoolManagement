using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Interfaces
{
	public interface IAddressRepository : IRepository<Address>
	{
		Task<IEnumerable<Address>> GetAddressesByCompany(Guid companyId);
		Task<IEnumerable<Address>> GetAddressesByContact(Guid contactId);
	}
}
