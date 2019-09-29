using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Interfaces
{
	public interface IPhoneRepository : IRepository<Phone>
	{
		Task<IEnumerable<Phone>> GetPhonesByCompany(Guid companyId);
		Task<IEnumerable<Phone>> GetPhonesByContact(Guid contactId);
	}
}
