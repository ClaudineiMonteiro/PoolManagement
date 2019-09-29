using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vm.Pm.Business.Interfaces;
using Vm.Pm.Business.Models;
using Vm.Pm.Data.Context;

namespace Vm.Pm.Data.Repository
{
	public class AddressRepository : BaseRepository<Address>, IAddressRepository
	{
		public AddressRepository(PoolManagementDbContext db): base(db) { }

		public async Task<IEnumerable<Address>> GetAddressesByCompany(Guid companyId)
		{
			return await Db.Addresses.AsNoTracking().Where(c => c.CompanyId == companyId).ToListAsync();
		}

		public async Task<IEnumerable<Address>> GetAddressesByContact(Guid contactId)
		{
			return await Db.Addresses.AsNoTracking().Where(c => c.ContactId == contactId).ToListAsync();
		}
	}
}
