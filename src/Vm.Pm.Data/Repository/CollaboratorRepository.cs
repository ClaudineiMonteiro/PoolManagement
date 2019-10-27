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
	public class CollaboratorRepository : BaseRepository<Collaborator>, ICollaboratorRepository
	{
		public CollaboratorRepository(PoolManagementDbContext db) : base(db) { }


		public async Task<Collaborator> GetCollaboratorAddresses(Guid id)
		{
			return await Db.Collaborators
				.AsNoTracking()
				.Include(a => a.Addresses)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Collaborator> GetCollaboratorPhones(Guid id)
		{
			return await Db.Collaborators
				.AsNoTracking()
				.Include(a => a.Phones)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Collaborator> GetCollaboratorPhonesAddresses(Guid id)
		{
			return await Db.Collaborators
				.AsNoTracking()
				.Include(a => a.Phones)
				.Include(a => a.Addresses)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<IEnumerable<Collaborator>> GetCollaboratorsByCompany(Guid companyId)
		{
			return await Db.Collaborators
				.AsNoTracking()
				.Include(p => p.Phones)
				.Include(a => a.Addresses)
				.Where(c => c.CompanyId == companyId).ToListAsync();
		}

		public async override Task<List<Collaborator>> GetAll()
		{
			return await Db.Collaborators
				.AsNoTracking()
				.Include(c => c.Company)
				.OrderBy(o => o.Name)
				.ToListAsync();
		}
	}
}
