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
	public class PhoneRepository : BaseRepository<Phone>, IPhoneRepository
	{
		public PhoneRepository(PoolManagementDbContext db) : base(db) { }

		public async Task<IEnumerable<Phone>> GetPhonesByCompany(Guid companyId)
		{
			return await Db.Phones.AsNoTracking().Where(p => p.CompanyId == companyId).ToListAsync();
		}

		public async Task<IEnumerable<Phone>> GetPhonesByContact(Guid contactId)
		{
			return await Db.Phones.AsNoTracking().Where(p => p.ContactId == contactId).ToListAsync();
		}
	}
}
