using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Vm.Pm.Business.Interfaces;
using Vm.Pm.Business.Models;
using Vm.Pm.Data.Context;

namespace Vm.Pm.Data.Repository
{
	public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
	{
		public CompanyRepository(PoolManagementDbContext db) : base(db) { }

		public async Task<Company> GetCompanyAddresses(Guid id)
		{
			return await Db.Companies
				.Include(a => a.Addresses)
				.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Company> GetCompanyContacts(Guid id)
		{
			return await Db.Companies
				.Include(c => c.Contacts)
				.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Company> GetCompanyContactsAddresses(Guid id)
		{
			return await Db.Companies
				.Include(c => c.Contacts)
				.Include(a => a.Addresses)
				.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Company> GetCompanyContactsPhones(Guid id)
		{
			return await Db.Companies
				.Include(c => c.Contacts)
				.Include(p => p.Phones)
				.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Company> GetCompanyContactsPhonesAddresses(Guid id)
		{
			return await Db.Companies
				.Include(c => c.Contacts)
				.Include(p => p.Phones)
				.Include(a => a.Addresses)
				.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Company> GetCompanyPhones(Guid id)
		{
			return await Db.Companies
				.Include(p => p.Phones)
				.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Company> GetCompanyPhonesAddresses(Guid id)
		{
			return await Db.Companies
				.Include(p => p.Phones)
				.Include(a => a.Addresses)
				.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
		}
	}
}
