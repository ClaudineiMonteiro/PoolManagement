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
	public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
	{
		public CustomerRepository(PoolManagementDbContext db) : base(db) { }

		public async Task<Customer> GetCustomerAddresses(Guid id)
		{
			return await Db.Customers
				.AsNoTracking()
				//.Include(a => a.Addresses)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Customer> GetCustomerCollaborators(Guid id)
		{
			return await Db.Customers
				.AsNoTracking()
				//.Include(c => c.CollaboratorCustomers)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Customer> GetCustomerContacts(Guid id)
		{
			return await Db.Customers
				.AsNoTracking()
				//.Include(c => c.Contacts)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Customer> GetCustomerPhones(Guid id)
		{
			return await Db.Customers
				.AsNoTracking()
				//.Include(p => p.Phones)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Customer> GetCustomerPhonesAddressesContactsCollaborators(Guid id)
		{
			return await Db.Customers
				.AsNoTracking()
				//.Include(p => p.Phones)
				//.Include(a => a.Addresses)
				//.Include(c => c.Contacts)
				//.Include(c => c.CollaboratorCustomers)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<IEnumerable<Customer>> GetCustomersByCollaborator(Guid collaboratorId)
		{
			return await Db.Customers
				.AsNoTracking()
				//.Include(cc => cc.CollaboratorCustomers)
				//.Where(c => c.CollaboratorCustomers.FirstOrDefault(p => p.CollaboratorId == collaboratorId).CollaboratorId == collaboratorId)
				.ToListAsync();
		}

		public async Task<IEnumerable<Customer>> GetCustomersByCompany(Guid companyId)
		{
			return await Db.Customers
				.AsNoTracking()
				.Where(c => c.CompanyId == companyId)
				.ToListAsync();
		}
	}
}
