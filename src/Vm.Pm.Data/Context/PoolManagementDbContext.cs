using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vm.Pm.Business.Models;

namespace Vm.Pm.Data.Context
{
	public class PoolManagementDbContext : DbContext
	{
		public PoolManagementDbContext(DbContextOptions options) : base(options) { }

		public DbSet<Address> Addresses { get; set; }
		public DbSet<Phone> Phones { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<Collaborator> Collaborators { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)))) property.Relational().ColumnType = "varchar(100)";

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(PoolManagementDbContext).Assembly);

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

			base.OnModelCreating(modelBuilder);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("RegistrationDate") != null))
			{
				if (entry.State == EntityState.Added)
				{
					entry.Property("RegistrationDate").CurrentValue = DateTime.Now;
					entry.Property("LastUpdatedDate").IsModified = false;
				}

				if (entry.State == EntityState.Modified)
				{
					entry.Property("RegistrationDate").IsModified = false;
					entry.Property("LastUpdatedDate").CurrentValue = DateTime.Now;

				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
