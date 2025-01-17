﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vm.Pm.Business.Interfaces;
using Vm.Pm.Business.Models;
using Vm.Pm.Data.Context;

namespace Vm.Pm.Data.Repository
{
	public class ContactRepository : BaseRepository<Contact>, IContactRepository
	{
		public ContactRepository(PoolManagementDbContext db) : base(db) { }

		public async  Task<Contact> GetContactAddresses(Guid id)
		{
			return await Db.Contacts
				.AsNoTracking()
				.Include(a => a.Adresses)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Contact> GetContactPhones(Guid id)
		{
			return await Db.Contacts
				.AsNoTracking()
				.Include(p => p.Phones)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Contact> GetContactPhonesAddresses(Guid id)
		{
			return await Db.Contacts
				.AsNoTracking()
				.Include(p => p.Phones)
				.Include(a => a.Adresses)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<IEnumerable<Contact>> GetContactsByCompany(Guid companyId)
		{
			return await Db.Contacts
							.AsNoTracking()
							.Include(p => p.Phones)
							.Include(a => a.Adresses)
							.Where(c => c.CompanyId == companyId).ToListAsync();
		}
	}
}
