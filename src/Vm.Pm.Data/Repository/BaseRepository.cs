using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vm.Pm.Business.Interfaces;
using Vm.Pm.Business.Models;
using Vm.Pm.Data.Context;

namespace Vm.Pm.Data.Repository
{
	public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
	{
		protected readonly PoolManagementDbContext Db;
		protected readonly DbSet<TEntity> DbSet;

		public BaseRepository(PoolManagementDbContext db)
		{
			Db = db;
			DbSet = db.Set<TEntity>();
		}

		public virtual async Task Add(TEntity entity)
		{
			DbSet.Add(entity);
			await SaveChanges();
		}

		public virtual async Task<List<TEntity>> GetAll()
		{
			return await DbSet.ToListAsync();
		}

		public virtual async Task<TEntity> GetById(Guid id)
		{
			TEntity entity = await DbSet.FindAsync(id);
			return entity;
		}

		public virtual async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
		{
			return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
		}

		public virtual async Task Update(TEntity entity)
		{
			DbSet.Update(entity);
			await SaveChanges();
		}

		public virtual async Task Remove(Guid id)
		{
			DbSet.Remove(new TEntity { Id = id });
			await SaveChanges();
		}

		public async Task<int> SaveChanges()
		{
			return await Db.SaveChangesAsync();
		}

		public void Dispose()
		{
			Db?.Dispose();
		}
	}
}
