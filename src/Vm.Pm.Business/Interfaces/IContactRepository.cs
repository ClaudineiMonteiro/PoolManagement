﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Business.Interfaces
{
	public interface IContactRepository : IRepository<Contact>
	{
		Task<IEnumerable<Contact>> GetContactsByCompany(Guid companyId);
	}
}